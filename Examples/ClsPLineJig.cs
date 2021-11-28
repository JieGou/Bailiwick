using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.Examples
{

    public class MyPlineCmds
    {

        class PlineJig : EntityJig
        {

            // Use a separate variable for the most recent point...
            // Again, not strictly necessary, but easier to reference

            Point3d m_tempPoint;

            Plane m_plane;

            bool m_isArcSeg = false;

            bool m_isUndoing = false;


            // At this stage, weour arc segments will
            // have a fixed bulge of 1.0...
            // Later we may update the routine to determine
            // the bulge based on the relative location
            // of the cursor

            const double kBulge = 1.0;


            public PlineJig(Matrix3d ucs)
              : base(new Polyline())
            {
                // Create a temporary plane, to help with calcs

                Point3d origin = new Point3d(0, 0, 0);

                Vector3d normal = new Vector3d(0, 0, 1);

                normal = normal.TransformBy(ucs);

                m_plane = new Plane(origin, normal);


                // Create polyline, set defaults, add dummy vertex

                Polyline pline = Entity as Polyline;

                pline.SetDatabaseDefaults();

                pline.Normal = normal;

                AddDummyVertex();
            }


            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                JigPromptPointOptions jigOpts =

                  new JigPromptPointOptions();

                jigOpts.UserInputControls =
                  (UserInputControls.Accept3dCoordinates |
                  UserInputControls.NullResponseAccepted |
                  UserInputControls.NoNegativeResponseAccepted
                  );


                m_isUndoing = false;

                Polyline pline = Entity as Polyline;

                if (pline.NumberOfVertices == 1)
                {
                    // For the first vertex, just ask for the point

                    jigOpts.Message =
                      "\nStart point of polyline: ";
                }
                else if (pline.NumberOfVertices > 1)
                {
                    // For subsequent vertices, use a base point
                    if (m_isArcSeg)
                    {
                        jigOpts.SetMessageAndKeywords(
                          "\nSpecify endpoint of arc or [Line/Undo]: ",
                          "Line Undo"
                        );
                    }
                    else
                    {
                        jigOpts.SetMessageAndKeywords(
                          "\nSpecify next point or [Arc/Undo]: ",
                          "Arc Undo"
                        );
                    }
                }
                else // should never happen
                    return SamplerStatus.Cancel;


                // Get the point itself
                PromptPointResult res =
                  prompts.AcquirePoint(jigOpts);


                if (res.Status == PromptStatus.Keyword)
                {
                    if (res.StringResult == "Arc")
                    {
                        m_isArcSeg = true;
                    }
                    else if (res.StringResult == "Line")
                    {
                        m_isArcSeg = false;
                    }
                    else if (res.StringResult == "Undo")
                    {
                        m_isUndoing = true;
                    }

                    return SamplerStatus.OK;
                }
                else if (res.Status == PromptStatus.OK)
                {
                    // Check if it has changed or not
                    // (reduces flicker)

                    if (m_tempPoint == res.Value)
                    {
                        return SamplerStatus.NoChange;
                    }
                    else
                    {
                        m_tempPoint = res.Value;
                        return SamplerStatus.OK;
                    }
                }
                return SamplerStatus.Cancel;
            }


            protected override bool Update()
            {
                // Update the dummy vertex to be our
                // 3D point projected onto our plane

                Polyline pline = Entity as Polyline;

                pline.SetPointAt(
                  pline.NumberOfVertices - 1,
                  m_tempPoint.Convert2d(m_plane)
                );


                // If it's supposed to be an arc segment,
                // set the bulge appropriately

                if (m_isArcSeg)
                {
                    pline.SetBulgeAt(
                      pline.NumberOfVertices - 1,
                      kBulge
                    );
                }
                // Otherwise, it's a line, so set the bulge
                // to zero
                else
                {
                    pline.SetBulgeAt(
                      pline.NumberOfVertices - 1,
                      0
                    );
                }
                return true;
            }


            public Entity GetEntity()
            {
                return Entity;
            }

            public bool IsArcSegment()
            {
                return m_isArcSeg;
            }

            public bool IsUndoing()
            {
                return m_isUndoing;
            }
            
            public void AddDummyVertex()
            {
                // Create a new dummy vertex...
                // can have any initial value

                Polyline pline = Entity as Polyline;

                pline.AddVertexAt(
                  pline.NumberOfVertices,
                  new Point2d(0, 0),
                  0, 0, 0
                );
            }


            public void RemoveDummyVertex()
            {
                Polyline pline = Entity as Polyline;

                // Let's first remove our dummy vertex        

                if (pline.NumberOfVertices > 0)
                {
                    pline.RemoveVertexAt(pline.NumberOfVertices - 1);
                }

                // And then check the type of the last segment

                if (pline.NumberOfVertices >= 2)
                {
                    double blg =
                      pline.GetBulgeAt(pline.NumberOfVertices - 2);
                    m_isArcSeg = (blg != 0);
                }
            }


            public void AdjustSegmentType(bool isArc)
            {
                // Change the bulge of the previous segment,
                // if necessary

                double bulge = 0.0;

                if (isArc)
                    bulge = kBulge;

                Polyline pline = Entity as Polyline;

                if (pline.NumberOfVertices >= 2)
                    pline.SetBulgeAt(pline.NumberOfVertices - 2, bulge);

            }

        }


        [CommandMethod("GW_EX_MYPOLY")]
        public void MyPolyJig()
        {
            Document doc =                
                Application.DocumentManager.MdiActiveDocument;

            Editor ed = doc.Editor;


            // Get the current UCS, to pass to the Jig

            Matrix3d ucs =
              ed.CurrentUserCoordinateSystem;


            // Create our Jig object

            PlineJig jig = new PlineJig(ucs);


            // Loop to set the vertices directly on the polyline

            bool bPoint = false;
            bool bKeyword = false;
            bool bComplete = false;

            do
            {
                PromptResult res = ed.Drag(jig);

                bPoint =
                  (res.Status == PromptStatus.OK);

                // A new point was added

                if (bPoint)
                    jig.AddDummyVertex();

                bKeyword =
                  (res.Status == PromptStatus.Keyword);

                if (bKeyword)
                {
                    if (jig.IsUndoing())
                    {
                        jig.RemoveDummyVertex();
                    }
                    else
                    {
                        jig.AdjustSegmentType(jig.IsArcSegment());
                    }
                }

                // Null input terminates the command

                bComplete =
                  (res.Status == PromptStatus.None);

                if (bComplete)
                    // Let's clean-up the polyline before adding it
                    jig.RemoveDummyVertex();

            } while ((bPoint || bKeyword) && !bComplete);
            
            // If the jig completed successfully,
            // add the polyline

            if (bComplete)
            {
                Polyline pline = jig.GetEntity() as Polyline;

                if (pline.NumberOfVertices > 1)
                {
                    // Append entity
                    Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;

                    Transaction tr =
                      db.TransactionManager.StartTransaction();

                    using (tr)
                    {
                        BlockTable bt =
                          (BlockTable)tr.GetObject(
                            db.BlockTableId,
                            OpenMode.ForRead,
                            false
                          );

                        BlockTableRecord btr =
                          (BlockTableRecord)tr.GetObject(
                            bt[BlockTableRecord.ModelSpace],
                            OpenMode.ForWrite,
                            false
                          );

                        btr.AppendEntity(pline);

                        tr.AddNewlyCreatedDBObject(pline, true);

                        tr.Commit();

                    }
                }
            }
        }
    }
}
