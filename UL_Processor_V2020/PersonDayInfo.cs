using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UL_Processor_V2020
{
    public class PersonDayInfo
    {
        public String mapId = "";
        public String status = "ABSENT";
        public String lenaId = "";
        public String leftUbi = "";
        public String rightUbi = "";
        public Boolean present = false;
        public DateTime startDate = new DateTime(2000, 1, 1);
        public DateTime endDate = new DateTime(2000, 2, 1);

        public LenaVars totalLenaVars = new LenaVars();
        public LenaVars WUBILenaVars = new LenaVars();


        public Dictionary<String, LenaVars> totalLenaVarsAct = new Dictionary<string, LenaVars>();
       // public Dictionary<String, LenaVars> WUBILenaVarsAct = new Dictionary<string, LenaVars>();




        public PersonDayInfo()
        {
        }
        public PersonDayInfo(String commaLine, String id, DateTime sd, DateTime ed)
        {
            String[] line = commaLine.Split(',');
            try
            {
                mapId = id;
                lenaId = line[9].Trim();
                leftUbi = line[5].Trim();
                rightUbi = line[7].Trim();
                present = line[19].ToUpper() == "PRESENT";
                status = line[19].ToUpper();
                //DEBUG AND THEN DELETE
                try
                {
                    startDate = Convert.ToDateTime(line[11].Trim());
                    startDate = new DateTime(sd.Year, sd.Month, sd.Day, startDate.Hour, startDate.Minute, startDate.Second);
                }
                catch (Exception e)
                {
                    startDate = sd;
                }
                try
                {
                    endDate = Convert.ToDateTime(line[12].Trim());
                    endDate = new DateTime(ed.Year, ed.Month, ed.Day, endDate.Hour, endDate.Minute, endDate.Second);
                    if (endDate.Hour >= 1)
                        endDate=endDate.AddHours(12);
                }
                catch (Exception e)
                {
                    endDate = ed;
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
