using ArxLibertatisEditorIO.RawIO.FTL;

namespace ArxLibertatisEditorIO.MediumIO.FTL
{
    public class Ftl
    {
        public readonly PrimaryHeader primaryHeader = new PrimaryHeader();
        public DataSection3D dataSection3D = null;

        public void LoadFrom(FTL_IO ftl)
        {
            primaryHeader.LoadFrom(ref ftl.header);
            if (ftl.has3DDataSection)
            {
                dataSection3D ??= new DataSection3D();
                dataSection3D.LoadFrom(ftl._3DDataSection);
            }
            else
            {
                dataSection3D = null;
            }
        }

        public void SaveTo(FTL_IO ftl)
        {
            primaryHeader.SaveTo(ref ftl.header);

            if (dataSection3D != null)
            {
                ftl._3DDataSection ??= new FTL_IO_3D_DATA_SECTION();
                dataSection3D.SaveTo(ftl._3DDataSection);
                ftl.has3DDataSection = true;
            }
            else
            {
                ftl.has3DDataSection = false;
            }
        }
    }
}
