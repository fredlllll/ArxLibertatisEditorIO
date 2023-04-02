using ArxLibertatisEditorIO.RawIO.FTL;

namespace ArxLibertatisEditorIO.MediumIO.FTL
{
    public class Ftl
    {
        public readonly PrimaryHeader primaryHeader = new PrimaryHeader();
        public DataSection3D dataSection3D = null;

        public void LoadFrom(FTL_IO ftl)
        {
            primaryHeader.ReadFrom(ftl.header);
            if (ftl.has3DDataSection)
            {
                if (dataSection3D == null)
                {
                    dataSection3D = new DataSection3D();
                }
                dataSection3D.ReadFrom(ftl._3DDataSection);
            }
            else
            {
                dataSection3D = null;
            }
        }

        public void WriteTo(FTL_IO ftl)
        {
            primaryHeader.WriteTo(ref ftl.header);

            if (dataSection3D != null)
            {
                dataSection3D.WriteTo(ref ftl._3DDataSection);
                ftl.has3DDataSection = true;
            }
            else
            {
                ftl.has3DDataSection = false;
            }
        }
    }
}
