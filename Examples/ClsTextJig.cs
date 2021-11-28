using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.Runtime;
using System;

namespace MyFirstProject.Examples
{
    public class Commands
    {
        [CommandMethod("GW_EX_QT")]
        static public void QuickText()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptStringOptions pso = new PromptStringOptions("\nEnter text string");
            pso.AllowSpaces = true;
            PromptResult pr = ed.GetString(pso);

            if (pr.Status != PromptStatus.OK)
                return;

            Transaction tr = doc.TransactionManager.StartTransaction();
            using (tr)
            {
                BlockTableRecord btr =
                  (BlockTableRecord)tr.GetObject(
                    db.CurrentSpaceId, OpenMode.ForWrite
                  );

                // Create the text object, set its normal and contents

                DBText txt = new DBText();
                txt.Normal =
                  ed.CurrentUserCoordinateSystem.
                    CoordinateSystem3d.Zaxis;
                txt.TextString = pr.StringResult;

                // We'll add the text to the database before jigging
                // it - this allows alignment adjustments to be
                // reflected

                btr.AppendEntity(txt);
                tr.AddNewlyCreatedDBObject(txt, true);

                // Create our jig

                TextPlacementJig pj = new TextPlacementJig(tr, db, txt);

                // Loop as we run our jig, as we may have keywords

                PromptStatus stat = PromptStatus.Keyword;
                while (stat == PromptStatus.Keyword)
                {
                    PromptResult res = ed.Drag(pj);
                    stat = res.Status;
                    if (
                      stat != PromptStatus.OK &&
                      stat != PromptStatus.Keyword
                    )
                        return;
                }

                tr.Commit();
            }
        }


        class TextPlacementJig : EntityJig
        {
            // Declare some internal state

            Autodesk.AutoCAD.DatabaseServices.Database _db;
            Transaction _tr;
            Point3d _position;
            double _angle, _txtSize;
            bool _toggleBold, _toggleItalic;
            TextHorizontalMode _align;

            // Constructor
            // public TextPlacementJig(Transaction tr, Database db, Entity ent) : base(ent)
            public TextPlacementJig(Transaction tr, Autodesk.AutoCAD.DatabaseServices.Database db, Entity ent) : base(ent)
            {
                _db = db;
                _tr = tr;
                _angle = 0;
                _txtSize = 1;
            }

            protected override SamplerStatus Sampler(
              JigPrompts jp
            )
            {
                // We acquire a point but with keywords

                JigPromptPointOptions po =
                  new JigPromptPointOptions(
                    "\nPosition of text"
                  );

                po.UserInputControls =
                  (UserInputControls.Accept3dCoordinates |
                    UserInputControls.NullResponseAccepted |
                    UserInputControls.NoNegativeResponseAccepted |
                    UserInputControls.GovernedByOrthoMode);

                po.SetMessageAndKeywords(
                  "\nSpecify position of text or " +
                  "[Bold/Italic/LArger/Smaller/" +
                   "ROtate90/LEft/Middle/RIght]: ",
                  "Bold Italic LArger Smaller " +
                  "ROtate90 LEft Middle RIght"
                );

                PromptPointResult ppr = jp.AcquirePoint(po);

                if (ppr.Status == PromptStatus.Keyword)
                {
                    switch (ppr.StringResult)
                    {
                        case "Bold":
                            {
                                _toggleBold = true;
                                break;
                            }
                        case "Italic":
                            {
                                _toggleItalic = true;
                                break;
                            }
                        case "LArger":
                            {
                                // Multiple the text size by two

                                _txtSize *= 2;
                                break;
                            }
                        case "Smaller":
                            {
                                // Divide the text size by two

                                _txtSize /= 2;
                                break;
                            }
                        case "ROtate90":
                            {
                                // To rotate clockwise we subtract 90 degrees and
                                // then normalise the angle between 0 and 360

                                _angle -= Math.PI / 2;
                                while (_angle < Math.PI * 2)
                                {
                                    _angle += Math.PI * 2;
                                }
                                break;
                            }
                        case "LEft":
                            {
                                _align = TextHorizontalMode.TextLeft;
                                break;
                            }
                        case "RIght":
                            {
                                _align = TextHorizontalMode.TextRight;
                                break;
                            }
                        case "Middle":
                            {
                                _align = TextHorizontalMode.TextMid;
                                break;
                            }
                    }

                    return SamplerStatus.OK;
                }
                else if (ppr.Status == PromptStatus.OK)
                {
                    // Check if it has changed or not (reduces flicker)

                    if (
                      _position.DistanceTo(ppr.Value) <
                        Tolerance.Global.EqualPoint
                    )
                        return SamplerStatus.NoChange;

                    _position = ppr.Value;
                    return SamplerStatus.OK;
                }

                return SamplerStatus.Cancel;
            }

            protected override bool Update()
            {
                // Set properties on our text object

                DBText txt = (DBText)Entity;

                txt.Position = _position;
                txt.Height = _txtSize;
                txt.Rotation = _angle;
                txt.HorizontalMode = _align;
                if (_align != TextHorizontalMode.TextLeft)
                {
                    txt.AlignmentPoint = _position;
                    txt.AdjustAlignment(_db);
                }

                // Set the bold and/or italic properties on the style

                if (_toggleBold || _toggleItalic)
                {
                    TextStyleTable tab =
                      (TextStyleTable)_tr.GetObject(
                        _db.TextStyleTableId, OpenMode.ForRead
                      );

                    TextStyleTableRecord style =
                      (TextStyleTableRecord)_tr.GetObject(
                        txt.TextStyleId, OpenMode.ForRead
                      );

                    // A bit convoluted, but this check will tell us
                    // whether the new style is bold/italic

                    bool bold = !(style.Font.Bold == _toggleBold);
                    bool italic = !(style.Font.Italic == _toggleItalic);
                    _toggleBold = false;
                    _toggleItalic = false;

                    // Get the new style name based on the old name and
                    // a suffix ("_BOLD", "_ITALIC" or "_BOLDITALIC")

                    var oldName = style.Name.Split(new[] { '_' });
                    string newName =
                      oldName[0] +
                      (bold || italic ? "_" +
                        (bold ? "BOLD" : "") +
                        (italic ? "ITALIC" : "")
                        : "");

                    // We only create a duplicate style if one doesn't
                    // already exist

                    if (tab.Has(newName))
                    {
                        txt.TextStyleId = tab[newName];
                    }
                    else
                    {
                        // We have to create a new style - clone the old one

                        TextStyleTableRecord newStyle =
                          (TextStyleTableRecord)style.Clone();

                        // Set a new name to avoid duplicate keys

                        newStyle.Name = newName;

                        // Create a new font based on the old one, but with
                        // our values for bold & italic

                        FontDescriptor oldFont = style.Font;
                        FontDescriptor newFont =
                          new FontDescriptor(
                            oldFont.TypeFace, bold, italic,
                            oldFont.CharacterSet, oldFont.PitchAndFamily
                          );

                        // Set it on the style

                        newStyle.Font = newFont;

                        // Add the new style to the text style table and
                        // the transaction

                        tab.UpgradeOpen();
                        ObjectId styleId = tab.Add(newStyle);
                        _tr.AddNewlyCreatedDBObject(newStyle, true);

                        // And finally set the new style on our text object

                        txt.TextStyleId = styleId;
                    }
                }

                return true;
            }
        }
    }
}