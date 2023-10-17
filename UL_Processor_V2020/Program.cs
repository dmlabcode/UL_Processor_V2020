using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


using System.Diagnostics;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace UL_Processor_V2020
{

    class Program
    {
        static String szVersion = "";

        public static void denoiseTest()//string cmd, string args)
        {
            string cmd = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", ""), "test2.py");
            string args = "E:\\UL_2122\\StarFish_2122\\09-02-2021\\MAPPINGS\\MAPPING_StarFish_2122.csv " +
                "E:\\UL_2122\\StarFish_2122\\09-02-2021\\Ubisense_Data\\MiamiLocation.2021-09-02_08-25-54-579_filtered.log " +
                "E:\\UL_2122\\StarFish_2122\\09-02-2021\\LENA_Data\\ITS\\ " +
                "E:\\UL_2122\\StarFish_2122\\09-02-2021\\Ubisense_Data_Denoised";
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "C:\\Users\\lcv31\\AppData\\Local\\Programs\\Python\\Python310\\python.exe";
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            //

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);

                    process.WaitForExit();
                    Console.Write(result);
                }
            }
        }

        public static void runPython(String fileName)
        {
            ScriptRuntimeSetup setup = Python.CreateRuntimeSetup(null);
            ScriptRuntime runtime = new ScriptRuntime(setup);
            ScriptEngine engine = Python.GetEngine(runtime);

            var paths = engine.GetSearchPaths();
            paths.Add(@"C:Program Files (x86)IronPython 2.7Lib");
            //paths.Add(@"C:Python27Lib"); // or you can add the CPython libs instead
            engine.SetSearchPaths(paths);



            ScriptSource source = engine.CreateScriptSourceFromFile(fileName);
            ScriptScope scope = engine.CreateScope();
            scope.SetVariable("path", "E:\\UL_2122\\StarFish_2122\\09-02-2021\\MAPPINGS\\MAPPING_StarFish_2122.csv");
            scope.SetVariable("path", "E:\\UL_2122\\StarFish_2122\\09-02-2021\\Ubisense_Data\\MiamiLocation.2021-09-02_08-25-54-579_filtered.log");
            source.Execute(scope);


            /*
            First is the mapping file of the observation.

            Second is the Ubi data (.log file)

            Third is the corresponding its file

            And the last is the path to store the output.*/

            /*ScriptRuntimeSetup setup = Python.CreateRuntimeSetup(null);
            ScriptRuntime runtime = new ScriptRuntime(setup);
            ScriptEngine engine = Python.GetEngine(runtime);
            ScriptSource source = engine.CreateScriptSourceFromFile(fileName);
            ScriptScope scope = engine.CreateScope();
            scope.SetVariable("path", fileName);
            source.Execute(scope);
            */

        }
        static void MainTest(string[] arguments)//DELETE
        {


            // runPython(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", ""), "test2.py"));
            // runPython(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", ""), "prepare_classroom_motion_vocal_dataset.py"));

            // Utilities.mergeDayFiles("E:\\StarFish_2021\\SYNC\\ONSETS", "21_GR0_22_061621_1596558875.CSV", "DAYONSETS_21_GR0_22_061621_1596558875.CSV");
            /* 
           ARGUMENTS:
           DIR:
           CLASSNAME:
           GRMIN:
           GRMAX:
           HRMIN: (OPTIONAL)
           HRMAX: (OPTIONAL)
           MINMAX: (OPTIONAL)
           DAYS: (COMMA SEPARATED)
           HACKT1: (OPTIONAL)
           UBICLEANUP: (YES/NO OPTIONAL DEFAULT NO )
           DENOISE: (YES/NO OPTIONAL DEFAULT NO )
           USEDENOISED: (YES/NO OPTIONAL DEFAULT NO )
           PROCESS: (YES/NO OPTIONAL DEFAULT YES )

           */
            //String[] szClassroomsToProcess = arguments;// {"DIR:G://CLASSROOMS_OLD// CLASSNAME:APPLETREE_1819 LENAVERSION:SP FOLDERSTRUCTURE:DAY GR:0.2,2 DATES:8/30/2018,9/6/2018,9/13/2018,10/4/2018,10/11/2018,11/1/2018,11/8/2018,11/29/2018,12/6/2018,1/10/2019,2/7/2019,2/14/2019,3/7/2019,3/14/2019,4/4/2019,5/9/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES"} ;
            //String[] szClassroomsToProcess = { "DIR:G://CLASSROOMS_OLD// CLASSNAME:APPLETREE_1819 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:16 DAYS:8/30/2018,9/6/2018,9/13/2018,10/4/2018,10/11/2018,11/1/2018,11/8/2018,11/29/2018,12/6/2018,1/10/2019,2/7/2019,2/14/2019,3/7/2019,3/14/2019,4/4/2019,5/9/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" } ;
            //String[] szClassroomsToProcess = { "DIR:G://CLASSROOMS_OLD// CLASSNAME:APPLETREE_1819 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:16 DAYS:8/30/2018 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/3/2018,10/8/2018,11/28/2018,12/4/2018,1/8/2019,1/15/2019,2/5/2019,3/6/2019,3/15/2019,5/7/2019,5/14/2019,5/24/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/8/2018 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //10/3/2018,10/8/2018,11/28/2018,12/4/2018,1/8/2019
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/3/2018,10/8/2018,11/28/2018,12/4/2018,1/8/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_17// CLASSNAME:LADYBUGS1 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:3/3/2017,3/10/2017,3/17/2017,3/31/2017,4/7/2017,4/21/2017,4/28/2017,5/12/2017,5/19/2017,5/26/2017 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1718// CLASSNAME:LADYBUGS2 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/24/2017,11/3/2017,11/17/2017,12/14/2017,1/11/2018,2/2/2018,2/16/2018,3/13/2018,3/20/2018,5/1/2018,5/16/2018 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:5/14/2019,5/24/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/3/2018,10/8/2018,11/28/2018,12/4/2018,1/8/2019,1/15/2019,2/5/2019,3/6/2019,3/15/2019,5/7/2019,5/14/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:5/14/2019,5/24/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1718// CLASSNAME:LADYBUGS2 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/24/2017,11/3/2017,11/17/2017,12/14/2017,1/11/2018,2/2/2018,2/16/2018,3/13/2018,3/20/2018,5/1/2018,5/16/2018 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:G://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:12 DAYS:01/23/2019,01/25/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:10 MINMAX:59 DAYS:01/23/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_PM GRMIN:0.2 GRMAX:2 HRMIN:11 HRMAX:1 MINMAX:50 DAYS:01/25/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:C://LVL// CLASSNAME:Starfish_2021 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:11/24/2020" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD// CLASSNAME:APPLETREE_1819 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:16 DAYS:8/30/2018,9/6/2018,9/13/2018,10/4/2018,10/11/2018,11/1/2018,11/8/2018,11/29/2018,12/6/2018,1/10/2019,2/7/2019,2/14/2019,3/7/2019,3/14/2019,4/4/2019,5/9/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:F:// CLASSNAME:StarFish_2021 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:13 DAYS:3/16/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:12 DAYS:01/23/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E:// CLASSNAME:StarFish_2021 JUSTUBICLEANUP:YES GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:3/16/2021,4/6/2021,4/13/2021,4/27/2021,5/20/2021,5/25/2021,6/8/2021,6/23/2021,7/28/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E:// CLASSNAME:StarFish_2021 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:3/16/2021,4/6/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };

            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_PM GRMIN:0.2 GRMAX:2 HRMIN:11 HRMAX:1 MINMAX:50 DAYS:01/25/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:01/23/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://UL_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:9/2/2021,10/5/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://UL_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:9/2/2021,10/5/2021,10/7/2021,11/2/2021,11/8/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://UL_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:9/2/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://UL_2021// CLASSNAME:StarFish_2021 JUSTUBICLEANUP:NO GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:3/16/2021,4/6/2021,4/13/2021,4/27/2021,5/20/2021,5/25/2021,6/8/2021,6/23/2021,7/28/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:D://UL_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:9/2/2021,10/5/2021,10/7/2021,11/2/2021,11/8/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:E://UL_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:9/2/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:01/23/2019 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
           // String[] szClassroomsToProcess = { "DIR:E://UL_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:9/2/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            String[] szClassroomsToProcess = { "DIR:E:// CLASSNAME:StarFish_2021 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:3/16/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };


            /******** A)FOR EACH CLASSROOM:********/
            foreach (String szClassroomArgs in szClassroomsToProcess)
            {
                Boolean toDenoise = true;// false;// true;// false;
                Boolean toProcess = true;// false;// true;// false;

                /*1- Create Classroom Object, read and set Parameters*/
                Classroom classRoom = new Classroom();
                classRoom.reDenoise = true;
                classRoom.useDenoised = true;// false;// true;// false;
                classRoom.ubiCleanup = true;// false;// true;// false;
                String[] args = szClassroomArgs.Split(' ');
                foreach (String arg in args)
                {
                    String[] setting = arg.Split(':');
                    if (setting.Length > 1)
                    {
                        switch (setting[0].Trim())
                        {
                            case "UBICLEANUP":
                                classRoom.ubiCleanup = setting[1].Trim().ToUpper() == "YES";
                                break;
                            case "REDENOISE":
                                classRoom.reDenoise = setting[1].Trim().ToUpper() == "YES";
                                break;
                            case "DENOISE":
                                classRoom.useDenoised = setting[1].Trim().ToUpper() == "YES";
                                break;
                            case "PROCESS":
                                toProcess = setting[1].Trim().ToUpper() == "YES";
                                break;
                            case "DIR":
                                classRoom.dir = setting[1].Trim() + ":" + setting[2].Trim();
                                break;
                            case "ANGLE":
                                classRoom.angle = Convert.ToDouble(setting[1].Trim());
                                break;
                            case "CLASSNAME":
                                classRoom.className = setting[1].Trim();
                                break;
                            case "GRMIN":
                                classRoom.grMin = Convert.ToDouble(setting[1].Trim());
                                break;
                            case "GRMAX":
                                classRoom.grMax = Convert.ToDouble(setting[1].Trim());
                                break;
                            case "HRMIN":
                                classRoom.startHour = Convert.ToInt16(setting[1].Trim());
                                break;
                            case "HRMAX":
                                classRoom.endHour = Convert.ToInt16(setting[1].Trim());
                                break;
                            case "MINMAX":
                                classRoom.endMinute = Convert.ToInt16(setting[1].Trim());
                                break;
                            case "DAYS":
                                foreach (String szDate in setting[1].Trim().Split(','))
                                {
                                    classRoom.classRoomDays.Add(Utilities.getDate(szDate));
                                }
                                break;
                        }
                    }
                }



                /*2- Set Version Name extension for file naming: GR+minGrwith_insteadOfDots+maxGrwith_insteadOfDots+TodaysMMDDYY+RANDOMNUMBER
                     Set Classroom Object mapId to link mapping files and data
                     Create directories for distinct reports*/


                Utilities.setVersion(classRoom.grMin, classRoom.grMax);//run day and GR version for file naming
                classRoom.mapById = "LONGID";
                classRoom.setDirs();

                /*3- Set Classroom’s Base Mappings */
                classRoom.setBaseMappings();


            }
        }
        static void addGPtoFile(String originalFile, String gpFile)
        {
            if (File.Exists(originalFile) && File.Exists(gpFile))
            {
                Dictionary<String, String> valsGp = new Dictionary<string, string>();
                Dictionary<String, Tuple<String, String, String>> valsTime = new Dictionary<string, Tuple<String, String, String>>();
                String szGpHeaders = "";
                using (StreamReader sr = new StreamReader(gpFile))
                {

                    String commaLine;
                    String[] line;
                    String[] headers;
                    List<int> gps = new List<int>();
                    int st = -1;
                    int pt = -1;
                    int rt = -1;
                    if (!sr.EndOfStream)
                    {
                        commaLine = sr.ReadLine();
                        headers = commaLine.Split(',');
                        int c = 0;
                        foreach (String h in headers)
                        {
                            if (h.ToUpper().StartsWith("UNSTRUCTURED_") || h.ToUpper().StartsWith("LEAD_UNSTRUCTURED_") ||
                                h.ToUpper().StartsWith("GP_") || h.ToUpper().StartsWith("LEAD_GP_") ||
                                h.ToUpper().StartsWith("M_") || h.ToUpper().StartsWith("LEAD_M_"))
                            {
                                gps.Add(c);
                                szGpHeaders += (h.ToUpper() + ",");
                            }


                            if (h.ToUpper().Trim() == "SUBJECTTIME")
                            {
                                st = c;
                            }

                            if (h.ToUpper().Trim() == "PARTNERTIME")
                            {
                                pt = c;
                            }

                            if (h.ToUpper().Trim() == "TOTALRECORDINGTIME")
                            {
                                rt = c;
                            }
                            c++;

                        }
                    }
                    int rowCount = 0;
                    while ((!sr.EndOfStream))// && lineCount < 10000)
                    {
                        rowCount++;
                        commaLine = sr.ReadLine();
                        line = commaLine.Split(',');

                        String szKey = line[0].ToUpper() + "," +
                            line[1].ToUpper() + "," +
                            line[2].ToUpper();

                        String szValue = "";
                        foreach (int i in gps)
                        {
                            szValue += ((line.Length > i ? line[i] : "") + ",");
                        }
                        valsGp.Add(szKey, szValue);
                        if (line.Length >= st && line.Length >= pt && line.Length >= rt)
                        {
                            valsTime.Add(szKey, new Tuple<String, String, String>(line[st], line[pt], line[rt]));
                        }
                        else
                        {
                            bool stop = true;
                        }
                    }


                }


                using (TextWriter sw = new StreamWriter(originalFile.Replace(".", "_ACTIVITIES.")))
                {
                    using (StreamReader sr = new StreamReader(originalFile))
                    {

                        String commaLine;
                        String[] line;
                        String[] headers;
                        int st = -1;
                        int pt = -1;
                        int rt = -1;
                        if (!sr.EndOfStream)
                        {
                            commaLine = sr.ReadLine();
                            sw.WriteLine(commaLine + "," + szGpHeaders);
                            headers = commaLine.Split(',');
                            int c = 0;
                            foreach (String h in headers)
                            {
                                
                                if (h.ToUpper().Trim() == "SUBJECTTIME")
                                {
                                    st = c;
                                }

                                if (h.ToUpper().Trim() == "PARTNERTIME")
                                {
                                    pt = c;
                                }

                                if (h.ToUpper().Trim() == "TOTALRECORDINGTIME")
                                {
                                    rt = c;
                                }
                                c++;

                            }
                        }
                         
                        while ((!sr.EndOfStream))// && lineCount < 10000)
                        {
                            commaLine = sr.ReadLine();
                            line = commaLine.Split(',');
                            String newCommaLine = "";
                            int c = 0;
                            String szKey = line[0].ToUpper() + "," +
                                line[1].ToUpper() + "," +
                                line[2].ToUpper();

                            foreach (String val in line)
                            {
                                if(c==st)
                                {
                                    if (valsTime.ContainsKey(szKey))
                                    {
                                        newCommaLine += valsTime[szKey].Item1;
                                    }
                                    else
                                    {
                                        newCommaLine += line[c];
                                    }
                                }
                                else if (c == pt)
                                {
                                    if (valsTime.ContainsKey(szKey))
                                    {
                                        newCommaLine += valsTime[szKey].Item2;
                                    }
                                    else
                                    {
                                        newCommaLine += line[c];
                                    }
                                }
                                else if (c == rt)
                                {
                                    if (valsTime.ContainsKey(szKey))
                                    {
                                        newCommaLine += valsTime[szKey].Item3;
                                    }
                                    else
                                    {
                                        newCommaLine += line[c];
                                    }
                                }
                                else
                                {
                                    newCommaLine += line[c];
                                }
                                c++;
                                newCommaLine += ",";
                            }
                             
                            if (valsGp.ContainsKey(szKey))
                            {
                                sw.WriteLine(newCommaLine + "," + valsGp[szKey]);
                            }
                        }
                    }


                }

            }
        }
        static void Main(string[] arguments)
        {

            // runPython(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", ""), "test2.py"));
            // runPython(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", ""), "prepare_classroom_motion_vocal_dataset.py"));

            // Utilities.mergeDayFiles("E:\\StarFish_2021\\SYNC\\ONSETS", "21_GR0_22_061621_1596558875.CSV", "DAYONSETS_21_GR0_22_061621_1596558875.CSV");
            /* 
           ARGUMENTS:
           DIR:
           CLASSNAME:
           GRMIN:
           GRMAX:
           HRMIN: (OPTIONAL)
           HRMAX: (OPTIONAL)
           MINMAX: (OPTIONAL)
           DAYS: (COMMA SEPARATED)
           HACKT1: (OPTIONAL)
           UBICLEANUP: (YES/NO OPTIONAL DEFAULT NO )
           DENOISE: (YES/NO OPTIONAL DEFAULT NO )
           USEDENOISED: (YES/NO OPTIONAL DEFAULT NO )
           PROCESS: (YES/NO OPTIONAL DEFAULT YES )

           */
            //String[] szClassroomsToProcess = arguments;// {"DIR:G://CLASSROOMS_OLD// CLASSNAME:APPLETREE_1819 LENAVERSION:SP FOLDERSTRUCTURE:DAY GR:0.2,2 DATES:8/30/2018,9/6/2018,9/13/2018,10/4/2018,10/11/2018,11/1/2018,11/8/2018,11/29/2018,12/6/2018,1/10/2019,2/7/2019,2/14/2019,3/7/2019,3/14/2019,4/4/2019,5/9/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES"} ;
            //String[] szClassroomsToProcess = { "DIR:G://CLASSROOMS_OLD// CLASSNAME:APPLETREE_1819 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:16 DAYS:8/30/2018,9/6/2018,9/13/2018,10/4/2018,10/11/2018,11/1/2018,11/8/2018,11/29/2018,12/6/2018,1/10/2019,2/7/2019,2/14/2019,3/7/2019,3/14/2019,4/4/2019,5/9/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" } ;
            //String[] szClassroomsToProcess = { "DIR:G://CLASSROOMS_OLD// CLASSNAME:APPLETREE_1819 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:16 DAYS:8/30/2018 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/3/2018,10/8/2018,11/28/2018,12/4/2018,1/8/2019,1/15/2019,2/5/2019,3/6/2019,3/15/2019,5/7/2019,5/14/2019,5/24/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/8/2018 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //10/3/2018,10/8/2018,11/28/2018,12/4/2018,1/8/2019
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/3/2018,10/8/2018,11/28/2018,12/4/2018,1/8/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_17// CLASSNAME:LADYBUGS1 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:3/3/2017,3/10/2017,3/17/2017,3/31/2017,4/7/2017,4/21/2017,4/28/2017,5/12/2017,5/19/2017,5/26/2017 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1718// CLASSNAME:LADYBUGS2 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/24/2017,11/3/2017,11/17/2017,12/14/2017,1/11/2018,2/2/2018,2/16/2018,3/13/2018,3/20/2018,5/1/2018,5/16/2018 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:5/14/2019,5/24/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/3/2018,10/8/2018,11/28/2018,12/4/2018,1/8/2019,1/15/2019,2/5/2019,3/6/2019,3/15/2019,5/7/2019,5/14/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:5/14/2019,5/24/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1718// CLASSNAME:LADYBUGS2 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/24/2017,11/3/2017,11/17/2017,12/14/2017,1/11/2018,2/2/2018,2/16/2018,3/13/2018,3/20/2018,5/1/2018,5/16/2018 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:G://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:12 DAYS:01/23/2019,01/25/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:10 MINMAX:59 DAYS:01/23/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_PM GRMIN:0.2 GRMAX:2 HRMIN:11 HRMAX:1 MINMAX:50 DAYS:01/25/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:C://LVL// CLASSNAME:Starfish_2021 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:11/24/2020" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD// CLASSNAME:APPLETREE_1819 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:16 DAYS:8/30/2018,9/6/2018,9/13/2018,10/4/2018,10/11/2018,11/1/2018,11/8/2018,11/29/2018,12/6/2018,1/10/2019,2/7/2019,2/14/2019,3/7/2019,3/14/2019,4/4/2019,5/9/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:F:// CLASSNAME:StarFish_2021 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:13 DAYS:3/16/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:12 DAYS:01/23/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E:// CLASSNAME:StarFish_2021 JUSTUBICLEANUP:YES GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:3/16/2021,4/6/2021,4/13/2021,4/27/2021,5/20/2021,5/25/2021,6/8/2021,6/23/2021,7/28/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E:// CLASSNAME:StarFish_2021 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:3/16/2021,4/6/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };

            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_PM GRMIN:0.2 GRMAX:2 HRMIN:11 HRMAX:1 MINMAX:50 DAYS:01/25/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:01/23/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://UL_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:9/2/2021,10/5/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://UL_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:9/2/2021,10/5/2021,10/7/2021,11/2/2021,11/8/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://UL_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:9/2/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://UL_2021// CLASSNAME:StarFish_2021 JUSTUBICLEANUP:NO GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:3/16/2021,4/6/2021,4/13/2021,4/27/2021,5/20/2021,5/25/2021,6/8/2021,6/23/2021,7/28/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:D://UL_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:9/2/2021,10/5/2021,10/7/2021,11/2/2021,11/8/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:E://UL_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:9/2/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:01/23/2019 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://UL_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:9/2/2021,10/5/2021,10/7/2021,11/2/2021,11/8/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://UL_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:9/2/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:E://UL_2122//PRIDE_2122// CLASSNAME:LEAP_AM_2122 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:11 DAYS:10/25/2021,11/19/2021,12/3/21,1/28/2022,2/25/2022,3/29/2022 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://UL_2021// CLASSNAME:StarFish_2021 JUSTUBICLEANUP:NO GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:3/16/2021,4/6/2021,4/13/2021,4/27/2021,5/20/2021,5/25/2021,6/8/2021,6/23/2021,7/28/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://UL_2021// CLASSNAME:StarFish_2021 JUSTUBICLEANUP:NO GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:4/6/2021,4/27/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS1920// CLASSNAME:TURTLES_1920 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:14 DAYS:11/13/2019,12/06/2019 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://UL_2021// CLASSNAME:StarFish_2021 JUSTUBICLEANUP:NO GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:3/16/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://UL_2021// CLASSNAME:StarFish_2021 JUSTUBICLEANUP:NO GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:4/6/2021,4/13/2021,4/27/2021,5/20/2021,5/25/2021,6/8/2021,6/23/2021,7/28/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS1920// CLASSNAME:TURTLES_1920 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:14 DAYS:11/13/2019,12/6/2019,1/24/2020,2/12/2020 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS1920// CLASSNAME:LEAP_AM_1920 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:11/01/2019,12/09/2019,01/30/2020,02/28/2020,03/09/2020 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS1920// CLASSNAME:LEAP_AM_1920 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:11/01/2019 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS1819// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:01/23/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:YES ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS1920// CLASSNAME:LEAP_AM_1920 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:11/01/2019,12/09/2019,01/30/2020,02/28/2020,03/09/2020 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS1819// CLASSNAME:PRIDE_LEAP_PM GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:01/25/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS1819// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:01/23/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:YES ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS1819// CLASSNAME:PRIDE_LEAP_PM GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:01/25/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS1920// CLASSNAME:TURTLES_1920 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:14 DAYS:11/13/2019,12/6/2019,1/24/2020,2/12/2020 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS1819// CLASSNAME:PRIDE_REVM_AM GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:01/30/2019,03/01/2019,03/19/2019,04/24/2019,5/28/2019 HACKT1:YES ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS1920// CLASSNAME:LEAP_AM_1920 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:11/01/2019,12/09/2019,01/30/2020,02/28/2020,03/09/2020 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS1920// CLASSNAME:TURTLES_1920 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:14 DAYS:11/13/2019,12/6/2019,1/24/2020,2/12/2020 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS1920// CLASSNAME:AVENGERS_1920 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:14 DAYS:1/23/2020,2/18/2020 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS1920// CLASSNAME:REVM_AM_1920 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:11/06/2019,12/11/2019,01/29/2020,02/26/2020 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS1920// CLASSNAME:LEAP_PM_1920 GRMIN:0.2 GRMAX:2 HRMIN:11 HRMAX:14 MINMAX:59 DAYS:11/01/2019,12/09/2019,01/30/2020,02/28/2020,03/09/2020 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS_2122// CLASSNAME:LEAP_AM_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:10/25/2021,11/19/2021,12/03/2021,01/28/2022,02/25/2022,03/29/2022,05/24/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS_2122// CLASSNAME:LEAP_AM_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:10 MINMAX:50 DAYS:10/25/2021,11/19/2021,12/03/2021,01/28/2022,02/25/2022,03/29/2022,05/24/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS_2122// CLASSNAME:LEAP_PM_2122 GRMIN:0.2 GRMAX:2 HRMIN:11 HRMAX:14 MINMAX:50 DAYS:10/25/2021,11/19/2021,12/03/2021,01/28/2022,02/25/2022,03/29/2022,05/24/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS_2122// CLASSNAME:LEAP_PM_2122 GRMIN:0.2 GRMAX:2 HRMIN:11 HRMAX:14 MINMAX:50 DAYS:10/25/2021 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS_2122// CLASSNAME:Room4_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:14 MINMAX:50 DAYS:12/06/2021,01/26/2022,02/09/2022,03/30/2022,04/29/2022,05/18/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //  // String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS_2122// CLASSNAME:Room4_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:14 MINMAX:50 DAYS:12/06/2021,01/26/2022,02/09/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };

            //String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS_2122// CLASSNAME:Turtles_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:09/30/2021,10/27/2021,11/10/2021,01/31/2022,02/24/2022,03/18/2022,04/13/2022,05/04/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };

            //String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS_2122// CLASSNAME:LadyBugs_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:09/09/2021,10/12/2021,10/14/2021 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:09/02/2021,10/05/2021,10/07/2021 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };



            /*******************LAST RUNS************************
             String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_2122// CLASSNAME:LadyBugs_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:" +
           "09/09/2021,10/12/2021,10/14/2021,11/09/2021,12/09/2021,12/14/2021,01/13/2022,01/14/2022,02/08/2022,02/10/2022,03/08/2022,03/14/2022,04/14/2022,04/19/2022,05/12/2022,07/12/2022,07/14/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
           
             String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:LEAP_AM_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:10 MINMAX:50 DAYS:10/25/2021,11/19/2021,12/03/2021,01/28/2022,02/25/2022,03/29/2022,05/24/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };

             *********************LAST RUNS***********************/
            // String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:LEAP_PM_2122 GRMIN:0.2 GRMAX:2 HRMIN:11 HRMAX:14 MINMAX:50 DAYS:10/25/2021,11/19/2021,12/03/2021,01/28/2022,02/25/2022,03/29/2022,05/24/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };

            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:Room4_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:14 MINMAX:50 DAYS:12/06/2021,01/26/2022,02/09/2022,03/30/2022,05/18/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:Avengers_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:10/06/2021,11/12/2021,12/10/2021,02/22/2022,04/06/2022,05/27/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:Avengers_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:11/12/2021 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };

            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:Avengers_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:11/12/2021 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS_2122// CLASSNAME:Room4_2122 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:14 MINMAX:50 DAYS:12/06/2021,01/26/2022,02/09/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:E://CLASSROOMS_2122// CLASSNAME:Room4_2122 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:14 MINMAX:50 DAYS:12/06/2021,01/26/2022,02/09/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2223// CLASSNAME:Turtles_2223 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:14 MINMAX:50 DAYS:02/28/2023 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //  String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:LEAP_PM_2122 GRMIN:0.2 GRMAX:2 HRMIN:11 HRMAX:14 MINMAX:50 DAYS:10/25/2021,11/19/2021,12/03/2021,01/28/2022,02/25/2022,03/29/2022,05/24/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:Turtles_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:09/30/2021,10/27/2021,11/10/2021,01/31/2022,02/24/2022,03/18/2022,04/13/2022,05/04/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };

            /****test runs ****/
            //  String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS1920// CLASSNAME:LEAP_AM_1920 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:11 MINMAX:50 DAYS:12/09/2019 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2223// CLASSNAME:LEAP_AM_2223 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:10 MINMAX:50 DAYS:02/10/2023 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };


            /*  String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:StarFish_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:" +
             "09/02/2021,10/05/2021,10/07/2021,11/02/2021,11/08/2021,12/02/2021,12/07/2021,01/11/2022,01/12/2022," +
             "02/11/2022,02/14/2022,03/01/2022,03/03/2022,04/05/2022,04/28/2022,05/03/2022,05/05/2022,06/07/2022," +
           "06/08/2022,07/05/2022,07/07/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };

              
            String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:LadyBugs_2122 GRMIN:0 GRMAX:1.5 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:" +
            //"03/14/2022,04/14/2022,04/19/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            "09/09/2021,10/12/2021,10/14/2021,11/09/2021,12/09/2021,12/14/2021,01/13/2022,01/14/2022,02/08/2022,02/10/2022,03/08/2022,03/14/2022,04/14/2022,04/19/2022,05/12/2022,07/12/2022,07/14/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            */


            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:Turtles_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:09/30/2021,10/27/2021,11/10/2021,01/31/2022,02/24/2022,03/18/2022,04/13/2022,05/04/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:LittleFish_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:09/22/2021,10/28/2021,11/17/2021,11/18/2021,01/18/2022,01/20/2022,02/15/2022,02/17/2022,03/15/2022,03/17/2022,05/04/2022,06/16/2022,07/21/2022,07/29/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
           //  String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:LittleFish_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:10/28/2021 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
          //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:LittleFish_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:01/18/2022,01/20/2022,02/15/2022,02/17/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_2122// CLASSNAME:LittleFish_2122 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:09/22/2021,10/28/2021,11/17/2021,11/18/2021,01/18/2022,01/20/2022,02/15/2022,02/17/2022,03/15/2022,03/17/2022,05/04/2022,06/16/2022,07/21/2022,07/29/2022 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };

            /******** A)FOR EACH CLASSROOM:********/
            foreach (String szClassroomArgs in szClassroomsToProcess)
            {
                Boolean toProcess = true;// false;// true;// false;

                /*1- Create Classroom Object, read and set Parameters*/
                Classroom classRoom = new Classroom();

                classRoom.useDenoised = true;// true;// true;// true;//true;// true;// false;// false;// true;// false;
                classRoom.ubiCleanup = true;//  true;// true;// false;// true;// false;
                classRoom.reDenoise = false;//true;// false;// true;// false;
                classRoom.addActivities = false;// true;// true ;// true;
              /*  classRoom.activityTypes.Add("unstructured");
                classRoom.activityTypes.Add("structured");
                classRoom.activityTypes.Add("GP");
                classRoom.activityTypes.Add("M");
                classRoom.activityTypes.Add("OuP");
                classRoom.activityTypes.Add("C");
                classRoom.activityTypes.Add("OrP");
                classRoom.activityTypes.Add("MD");*/
                //GP, OuP, M,C, OrP, MD

                String[] args = szClassroomArgs.Split(' ');
                
                foreach (String arg in args) 
                {
                    String[] setting = arg.Split(':');
                     
                    if (setting.Length > 1)
                    {
                        switch (setting[0].Trim())
                        {
                            case "DORECINFO":
                                classRoom.ubiCleanup = setting[1].Trim().ToUpper() == "YES";
                                break;
                            case "EXISTINGVERSION":
                                classRoom.processData = false;
                                Utilities.szVersion = setting[1].Trim();
                                break;
                            case "UBICLEANUP":
                                classRoom.ubiCleanup = setting[1].Trim().ToUpper() == "YES";
                                break;
                            case "ADDGP":
                                //  classRoom.addGp = setting[1].Trim().ToUpper() == "YES";
                                break;
                            case "REDENOISE":
                                classRoom.reDenoise = setting[1].Trim().ToUpper() == "YES";
                                break;
                            case "HACKT1":
                                classRoom.t1Hack = setting[1].Trim().ToUpper() == "YES";
                                break;
                            case "USEDENOISED":
                                classRoom.useDenoised = setting[1].Trim().ToUpper() == "YES";
                                break;
                            case "PROCESS":
                                toProcess = setting[1].Trim().ToUpper() == "YES";
                                break;
                            case "DIR":
                                classRoom.dir = setting[1].Trim() + ":" + setting[2].Trim();
                                break;
                            case "ANGLE":
                                classRoom.angle = Convert.ToDouble(setting[1].Trim());
                                break;
                            case "CLASSNAME":
                                classRoom.className = setting[1].Trim();
                                break;
                            case "GRMIN":
                                classRoom.grMin = Convert.ToDouble(setting[1].Trim());
                                break;
                            case "GRMAX":
                                classRoom.grMax = Convert.ToDouble(setting[1].Trim());
                                break;
                            case "HRMIN":
                                classRoom.startHour = Convert.ToInt16(setting[1].Trim());
                                break;
                            case "HRMAX":
                                classRoom.endHour = Convert.ToInt16(setting[1].Trim());
                                break;
                            case "MINMAX":
                                classRoom.endMinute = Convert.ToInt16(setting[1].Trim());
                                break;
                            case "DAYS":
                                foreach (String szDate in setting[1].Trim().Split(','))
                                {
                                    classRoom.classRoomDays.Add(Utilities.getDate(szDate));
                                }
                                classRoom.addActivities = true;
                                break;
                            case "ACTS":
                                foreach (String szAct in setting[1].Trim().Split(','))
                                {
                                    classRoom.activityTypes.Add(szAct);
                                }
                                break;
                        }
                    }
                }



                /*2- Set Version Name extension for file naming: GR+minGrwith_insteadOfDots+maxGrwith_insteadOfDots+TodaysMMDDYY+RANDOMNUMBER
                     Set Classroom Object mapId to link mapping files and data
                     Create directories for distinct reports*/


                if(Utilities.szVersion.Trim() == "")
                    Utilities.setVersion(classRoom.grMin, classRoom.grMax, classRoom.useDenoised, classRoom.activityTypes.Count>0);//run day and GR version for file naming
                
                classRoom.mapById = "LONGID";
                classRoom.setDirs();


               //Utilities.szVersion = "_GR0_22_120422_V2275477135";// PAIRACTIVITY_GR0_22_051722_1358347332 "10_21_2020_2098687227";// "10_20_2020_419130690";// "10_20_2020_986296434";// "10_19_2020_1345568271";//10_19_2020_1345568271  10_19_2020_1700354507
               //classRoom.getPairActLeadsFromFiles();




                /*3- Set Classroom’s Base Mappings */
                classRoom.setBaseMappings();
                //  addGPtoFile("D:\\CLASSROOMS1920\\TURTLES_1920\\SYNC\\PAIRACTIVITY_ALL_4TURTLES_1920_3_28_2021_187694180ALL.CSV", "D:\\CLASSROOMS1920\\TURTLES_1920\\SYNC\\PAIRACTIVITY\\PAIRACTIVITY_GR0_22_070822_1036404247ALL.CSV");//PAIRACTIVITY__GR0_22_051722_1358347332

               // addGPtoFile("D:\\CLASSROOMS1920\\TURTLES_1920\\SYNC\\PAIRACTIVITY_ALL_4TURTLES_1920_3_28_2021_187694180ALL.CSV", "D:\\CLASSROOMS1920\\TURTLES_1920\\SYNC\\PAIRACTIVITY\\PAIRACTIVITY_GR0_22_071122_123110340ALL.CSV");//PAIRACTIVITY__GR0_22_051722_1358347332



                //denoise();

                /*4 Clean ubi */
                if (classRoom.ubiCleanup)
                    classRoom.clean();
                 

                if (classRoom.useDenoised)
                {
                    classRoom.denoise(); //denoise();
                }

                /* 5 Process */
                if (toProcess)
                    classRoom.process(true, true);

                // classRoom.processGofRfiles();
                // classRoom.processFromGofRfiles("", true);
                //classRoom.processOnsetsGrAndActLogs(); TO DELETE


                classRoom.mergeDayFiles();



                //Utilities.szVersion = "10_26_2020_478216537";// "10_21_2020_2098687227";// "10_20_2020_419130690";// "10_20_2020_986296434";// "10_19_2020_1345568271";//10_19_2020_1345568271  10_19_2020_1700354507
                classRoom.getPairActLeadsFromFiles();
              //  addGPtoFile("D:\\CLASSROOMS1920\\TURTLES_1920\\SYNC\\PAIRACTIVITY_ALL_4TURTLES_1920_3_28_2021_187694180ALL.CSV", "D:\\CLASSROOMS1920\\TURTLES_1920\\SYNC\\PAIRACTIVITY\\PAIRACTIVITY_GR0_22_071122_123110340ALL.CSV");//PAIRACTIVITY__GR0_22_051722_1358347332




                //classRoom.process();
                //classRoom.mergeSimpleFiles();
                //classRoom.processTenthOfSecs();
            }

            Console.ReadLine();
        }
    }
}
