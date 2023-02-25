using System;
using System.Runtime.InteropServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace MyFirstProject.BW
{
    internal class NativeMethods
    {
        internal class ImportsR22
        {
            internal struct ads_name
            {
                private IntPtr a;
                private IntPtr b;
            };

            [DllImport("acdb22.dll",
                CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?acdbGetAdsName@@YA?AW4ErrorStatus@Acad@@AAY01JVAcDbObjectId@@@Z")]
            private static extern int AcdbGetAdsName32(ref ads_name name, ObjectId objId);

            [DllImport("acdb22.dll",
                CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?acdbGetAdsName@@YA?AW4ErrorStatus@Acad@@AEAY01_JVAcDbObjectId@@@Z")]
            private static extern int AcdbGetAdsName64(ref ads_name name, ObjectId objId);

            private static int AcdbGetAdsName(ref ads_name name, ObjectId objId)
            {
                if (Marshal.SizeOf(IntPtr.Zero) > 4)
                {
                    return AcdbGetAdsName64(ref name, objId);
                }

                return AcdbGetAdsName32(ref name, objId);
            }

            [DllImport("accore.dll",
                CharSet = CharSet.Unicode,
                CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "acdbEntGet")]
            private static extern System.IntPtr AcdbEntGet(ref ads_name ename);

            internal static System.Collections.Generic.List<TypedValue> AcdbEntGetTypedValues(ObjectId id)
            {
                var result = new System.Collections.Generic.List<TypedValue>();

                var name = new ads_name();

                int res = AcdbGetAdsName(ref name, id);

                ResultBuffer rb = new ResultBuffer();

                Autodesk.AutoCAD.Runtime.Interop.AttachUnmanagedObject(rb, AcdbEntGet(ref name), true);

                ResultBufferEnumerator iter = rb.GetEnumerator();

                while (iter.MoveNext())
                {
                    result.Add(iter.Current);
                }

                return result;
            }
        }
    }
}