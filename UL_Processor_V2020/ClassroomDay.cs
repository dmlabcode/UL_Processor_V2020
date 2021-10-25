using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
namespace UL_Processor_V2020
{
    
    public class ClassroomDay
    {
        public DateTime classDay;
        public Dictionary<String, Person> personBaseMappings = new Dictionary<string, Person>();
        public Dictionary<String, PersonDayInfo> personDayMappings = new Dictionary<string, PersonDayInfo>();
        public Dictionary<String, List<LenaOnset>> lenaOnsets = new Dictionary<string, List<LenaOnset>>();
        public Dictionary<String, List<UbiLocation>> ubiLocationsL = new Dictionary<string, List<UbiLocation>>();
        public Dictionary<String, List<UbiLocation>> ubiLocationsR = new Dictionary<string, List<UbiLocation>>();
        Dictionary<DateTime, Dictionary<String, PersonSuperInfo>> ubiTenths = new Dictionary<DateTime, Dictionary<string, PersonSuperInfo>>();

        public List<DateTime> maxTimes = new List<DateTime>();
        public List<Activity> logActivities = new List<Activity>();

        public double recSecs = 0;

        public ClassroomDay(DateTime day)
        {
            classDay = day;
        }
        public void writeSocialOnsetData(String className, String szOutputFile, List<String> diagnosisList, List<String> languagesList)
        { 
            TextWriter sw = new StreamWriter(szOutputFile);// countDays > 0);
            /*sw.WriteLine("File,Date,Subject,LenaID,SubjectType,segmentid," +
                "voctype,recstart,startsec,endsec,starttime,endtime,duration," +
                "seg_duration,wordcount,avg_db,avg_peak,turn_taking"
                //",logActivities,children,teachers");//,children,teachers"
                );*/



            sw.WriteLine("File,Date,Subject,LenaID,SubjectType,segmentid,voctype,recstart,startsec,endsec,starttime,endtime,duration,seg_duration,wordcount,avg_db,avg_peak,turn_taking,in_social_contact_talk ");

            foreach (String bid in lenaOnsets.Keys)
            {
                foreach (LenaOnset lo in lenaOnsets[bid])
                {
                     sw.WriteLine( 
                       lo.itsFile+ "," +
                                                                                            classDay + "," +
                                                                                            lo.id + "," +
                                                                                            lo.lenaId+"," +
                                                                                            lo.subjectType + "," +
                                                                                            lo.segmentId+"," +
                                                                                            lo.type + "," +
                                                                                            Utilities.getTimeStr(lo.recStartTime) + "," +
                                                                                            lo.startSec + "," +
                                                                                            lo.endSec + "," +
                                                                                            Utilities.getTimeStr(lo.startTime) + "," +
                                                                                            Utilities.getTimeStr(lo.endTime) + "," +
                                                                                            String.Format("{0:0.00}", lo.durSecs) + "," +
                                                                                            String.Format("{0:0.00}", lo.segmentDurSecs) + "," +
                                                                                            String.Format("{0:0.00}", lo.count) +","+
                                                                                            String.Format("{0:0.00}", lo.avgDb) + "," +
                                                                                            String.Format("{0:0.00}", lo.peakDb) + "," +
                                                                                            String.Format("{0:0.00}", lo.tc)+","+
                                                                                            (lo.inSocialContact?"YES":"NO")); 
                }

            }
        }
        public void writePairActivityData(Dictionary<String, Pair> pairs, String className, String szOutputFile, List<String> diagnosisList, List<String> languagesList)
        {
            TextWriter sw = new StreamWriter(szOutputFile); 


            String szHeader = 
"Date,Subject,Partner,SubjectShortID,PartnerShortID,SubjectDiagnosis,PartnerDiagnosis,SubjectLanguage,PartnerLanguage,"+
"SubjectGender,PartnerGender,Adult,SubjectStatus,PartnerStatus,SubjectType,PartnerType,"+
"Input1_pvc_or_sac,Input2_pvc_or_stc,"+
"Input3_dur_pvd_or_uttl,PairBlockTalking,PairTalkingDuration,"+
//taken out Subject-Talking-Duration-From_Start,"+Partner-Talking-Duration-From-Start,
"Subject-Talking-Duration-Evenly-Spread,Partner-Talking-Duration-Evenly-Spread,"+
"SubjectTurnCount,PartnerTurnCount,SubjectVocCount,PartnerVocCount,SubjectAdultCount,PartnerAdultCount,SubjectNoise,"+
"PartnerNoise,SubjectOLN,PartnerOLN,SubjectCry,PartnerCry,SubjectJoinedCry,PartnerJoinedCry,JoinedCry,PairProximityDuration,"+
"PairOrientation-ProximityDuration,SharedTimeinClassroom,SubjectTime,PartnerTime,TotalRecordingTime,WUBITotalVD,TotalVD,"+
"PartnerWUBITotalVD,PartnerTotalVD,WUBITotalVC,TotalVC,PartnerWUBITotalVC,PartnerTotalVC,WUBITotalTC,TotalTC,PartnerWUBITotalTC,"+
"PartnerTotalTC,WUBITotalAC,TotalAC,PartnerWUBITotalAC,PartnerTotalAC,WUBITotalNO,TotalNO,PartnerWUBITotalNO,PartnerTotalNO,"+
"WUBITotalOLN,TotalOLN,PartnerWUBITotalOLN,PartnerTotalOLN,WUBITotalCRY,TotalCRY,PartnerWUBITotalCRY,PartnerTotalCRY,"+
"WUBITotalAV_DB,TotalAV_DB,PartnerWUBITotalAV_DB,PartnerTotalAV_DB,WUBITotalAV_PEAK_DB,TotalAV_PEAK_DB,PartnerWUBITotalAV_PEAK_DB,PartnerTotalAV_PEAK_DB,CLASSROOM";
            //82

            String newDiagnosis = "";
            foreach(String d in diagnosisList)
            {
                newDiagnosis += ("Subject"+d + ",Partner" + d + ",");
            }
            String newLanguages = "";
            foreach (String l in languagesList)
            {
                newLanguages += newLanguages += ("Subject" + l + ",Partner" + l + ",");
            }

            newDiagnosis= newDiagnosis=="SubjectDiagnosis,PartnerDiagnosis,SubjectLanguage,PartnerLanguage,"? "SubjectDiagnosis,PartnerDiagnosis," : newDiagnosis;
            newLanguages = newLanguages == "" ? "SubjectLanguage,PartnerLanguage," : newLanguages;
            szHeader.Replace("SubjectDiagnosis,PartnerDiagnosis,", newDiagnosis);
            szHeader.Replace("SubjectLanguage,PartnerLanguage,", newLanguages);

            sw.WriteLine(szHeader);

            foreach (String szPair in pairs.Keys)
            {
                String szSubject = szPair.Split('|')[0];
                String szPartner = szPair.Split('|')[1];
                Person subject = personBaseMappings[szSubject];
                Person partner= personBaseMappings[szPartner];
                Pair pair = pairs[szPair];
                PersonDayInfo sdi = Utilities.getPerson(personDayMappings, szSubject);
                PersonDayInfo pdi = Utilities.getPerson(personDayMappings, szPartner);
                LenaVars subjectLenaVarsInContact = pair.subjectLenaVarsInContact;
                LenaVars partnerLenaVarsInContact = pair.partnerLenaVarsInContact;

                LenaVars subjectLenaVarsInWUBI = personDayMappings[szSubject].WUBILenaVars;
                LenaVars partnerLenaVarsInWUBI = personDayMappings[szPartner].WUBILenaVars;
                LenaVars subjectLenaVarsInTotal = personDayMappings[szSubject].totalLenaVars;
                LenaVars partnerLenaVarsInTotal = personDayMappings[szPartner].totalLenaVars;

                String newPairDiagnosis = "";
                String newPairDiagnosisP = "";
                int pos = 0;
                foreach (String d in subject.diagnosisList)
                {
                    newPairDiagnosis += (d + "," + partner.diagnosisList[pos] + ",");
                    newPairDiagnosisP += (partner.diagnosisList[pos] + "," + d + ",");
                    pos++;
                }
                String newPairLanguages = "";
                String newPairLanguagesP = "";
                pos = 0;
                foreach (String l in languagesList)
                {
                    newPairLanguages +=  ( l + "," + partner.languagesList[pos] + ",");
                    newPairLanguagesP += (partner.languagesList[pos] + "," + l+ ",");
                    pos++;
                }


                Console.WriteLine(this.classDay.Month + "/" + this.classDay.Day + "/" + this.classDay.Year + "," +
                subject.longId + "," +
                partner.longId + "," +
                subject.shortId + "," +
                partner.shortId + "," +
                newPairDiagnosis+
                newPairLanguages+
                subject.gender + "," +
                partner.gender + "," +
                (subject.longId.Contains("_L") || subject.longId.Contains("_T") || partner.longId.Contains("_L") || partner.longId.Contains("_T")) + "," +
                sdi.status + "," +
                pdi.status + "," +
                subject.subjectType + "," +
                partner.subjectType + "," +
                              (sdi.status.ToUpper() == "PRESENT" ? (subject.subjectType.ToUpper() == "CHILD" ? partnerLenaVarsInContact.totalChildUttCount.ToString() : subjectLenaVarsInContact.totalAdultWordCount.ToString()) : "NA" )+ "," +
                (sdi.status.ToUpper() == "PRESENT" ? (subject.subjectType.ToUpper() == "CHILD" ? partnerLenaVarsInContact.totalChildUttCount.ToString() : subjectLenaVarsInContact.totalChildUttCount.ToString()) : "NA" )+ "," +
                (sdi.status.ToUpper() == "PRESENT" ? (subject.subjectType.ToUpper() == "CHILD" ? partnerLenaVarsInContact.totalChildUttDuration.ToString() : subjectLenaVarsInContact.totalAdultWordCount.ToString()) : "NA") + "," +
                pair.pairBlockTalking+","+
                pair.pairProxOriDuration+","+//partnerLenaVarsInContact.totalChildUttDuration + "," +
//"NA," +//DEBUG FINISH UP?
//"NA," +//DEBUG FINISH UP?
                subjectLenaVarsInContact.totalChildUttDuration + "," +
                partnerLenaVarsInContact.totalChildUttDuration + "," +
                subjectLenaVarsInContact.totalTurnCounts + "," +
                partnerLenaVarsInContact.totalTurnCounts + "," +
                subjectLenaVarsInContact.totalChildUttCount + "," +
                partnerLenaVarsInContact.totalChildUttCount + "," +
                subjectLenaVarsInContact.totalAdultWordCount + "," +
                partnerLenaVarsInContact.totalAdultWordCount + "," +
                subjectLenaVarsInContact.totalNoise + "," +
                partnerLenaVarsInContact.totalNoise + "," +
                subjectLenaVarsInContact.totalOLN + "," +
                partnerLenaVarsInContact.totalOLN + "," +
                subjectLenaVarsInContact.totalChildCryDuration + "," +
                partnerLenaVarsInContact.totalChildCryDuration + "," +

                pair.joinedCry+","+
                pair.subjectJoinedCry+","+
                pair.partnerJoinedCry+","+
                pair.pairProxDuration+","+
                pair.pairProxOriDuration + "," +
                pair.sharedTimeInSecs + "," +
                pair.subjectTotalTimeInSecs + "," +
                pair.partnerTotalTimeInSecs + "," +
                recSecs+","+
                subjectLenaVarsInWUBI.totalChildUttDuration + "," +
                subjectLenaVarsInTotal.totalChildUttDuration + "," +
                partnerLenaVarsInWUBI.totalChildUttDuration + "," +
                partnerLenaVarsInTotal.totalChildUttDuration + "," +

                subjectLenaVarsInWUBI.totalChildUttCount + "," +
                subjectLenaVarsInTotal.totalChildUttCount + "," +
                partnerLenaVarsInWUBI.totalChildUttCount + "," +
                partnerLenaVarsInTotal.totalChildUttCount + "," +
                subjectLenaVarsInWUBI.totalTurnCounts + "," +
                subjectLenaVarsInTotal.totalTurnCounts + "," +
                partnerLenaVarsInWUBI.totalTurnCounts + "," +
                partnerLenaVarsInTotal.totalTurnCounts + "," +

                subjectLenaVarsInWUBI.totalAdultWordCount + "," +
                subjectLenaVarsInTotal.totalAdultWordCount + "," +
                partnerLenaVarsInWUBI.totalAdultWordCount + "," +
                partnerLenaVarsInTotal.totalAdultWordCount + "," +

                subjectLenaVarsInWUBI.totalNoise + "," +
                subjectLenaVarsInTotal.totalNoise + "," +
                partnerLenaVarsInWUBI.totalNoise + "," +
                partnerLenaVarsInTotal.totalNoise + "," +

                subjectLenaVarsInWUBI.totalOLN + "," +
                subjectLenaVarsInTotal.totalOLN + "," +
                partnerLenaVarsInWUBI.totalOLN + "," +
                partnerLenaVarsInTotal.totalOLN + "," +

                subjectLenaVarsInWUBI.totalChildCryDuration + "," +
                subjectLenaVarsInTotal.totalChildCryDuration + "," +
                partnerLenaVarsInWUBI.totalChildCryDuration + "," +
                partnerLenaVarsInTotal.totalChildCryDuration + "," +


                (subjectLenaVarsInWUBI.avgDb != 0 && subjectLenaVarsInWUBI.totalSegments != 0 ? subjectLenaVarsInWUBI.avgDb / subjectLenaVarsInWUBI.totalSegments : 0.00).ToString() + "," +
                (subjectLenaVarsInTotal.avgDb != 0 && subjectLenaVarsInTotal.totalSegments != 0 ? subjectLenaVarsInTotal.avgDb / subjectLenaVarsInTotal.totalSegments : 0.00).ToString() + "," +
                (partnerLenaVarsInWUBI.avgDb != 0 && partnerLenaVarsInWUBI.totalSegments != 0 ? partnerLenaVarsInWUBI.avgDb / partnerLenaVarsInWUBI.totalSegments : 0.00).ToString() + "," +
                (partnerLenaVarsInTotal.avgDb != 0 && partnerLenaVarsInTotal.totalSegments != 0 ? partnerLenaVarsInTotal.avgDb / partnerLenaVarsInTotal.totalSegments : 0.00).ToString() + "," +

                (subjectLenaVarsInWUBI.maxDb != 0 && subjectLenaVarsInWUBI.totalSegments != 0 ? subjectLenaVarsInWUBI.maxDb / subjectLenaVarsInWUBI.totalSegments : 0.00).ToString() + "," +
                (subjectLenaVarsInTotal.maxDb != 0 && subjectLenaVarsInTotal.totalSegments != 0 ? subjectLenaVarsInTotal.maxDb / subjectLenaVarsInTotal.totalSegments : 0.00).ToString() + "," +
                (partnerLenaVarsInWUBI.maxDb != 0 && partnerLenaVarsInWUBI.totalSegments != 0 ? partnerLenaVarsInWUBI.maxDb / partnerLenaVarsInWUBI.totalSegments : 0.00).ToString() + "," +
                (partnerLenaVarsInTotal.maxDb != 0 && partnerLenaVarsInTotal.totalSegments != 0 ? partnerLenaVarsInTotal.maxDb / partnerLenaVarsInTotal.totalSegments : 0.00).ToString() + "," +
                className );



                sw.WriteLine(this.classDay.Month + "/" + this.classDay.Day + "/" + this.classDay.Year + "," +
                subject.longId + "," +
                partner.longId + "," +
                subject.shortId + "," +
                partner.shortId + "," +
                newPairDiagnosis +
                newPairLanguages +
                subject.gender + "," +
                partner.gender + "," +
                (subject.longId.Contains("_L") || subject.longId.Contains("_T") || partner.longId.Contains("_L") || partner.longId.Contains("_T")) + "," +
                sdi.status + "," +
                pdi.status + "," +
                subject.subjectType + "," +
                partner.subjectType + "," +
                (sdi.status.ToUpper() == "PRESENT" ? (subject.subjectType.ToUpper() == "CHILD" ? partnerLenaVarsInContact.totalChildUttCount.ToString() : subjectLenaVarsInContact.totalAdultWordCount.ToString()) : "NA") + "," +
                (sdi.status.ToUpper() == "PRESENT" ? (subject.subjectType.ToUpper() == "CHILD" ? partnerLenaVarsInContact.totalChildUttCount.ToString() : subjectLenaVarsInContact.totalChildUttCount.ToString()) : "NA" )+ "," +
                (sdi.status.ToUpper() == "PRESENT" ? (subject.subjectType.ToUpper() == "CHILD" ? partnerLenaVarsInContact.totalChildUttDuration.ToString() : subjectLenaVarsInContact.totalAdultWordCount.ToString()) : "NA") + "," +
                pair.pairBlockTalking + "," +
                 pair.pairProxOriDuration + "," +//partnerLenaVarsInContact.totalChildUttDuration + "," +
//"NA," +
//"NA," +
                subjectLenaVarsInContact.totalChildUttDuration + "," +
                partnerLenaVarsInContact.totalChildUttDuration + "," +
                subjectLenaVarsInContact.totalTurnCounts + "," +
                partnerLenaVarsInContact.totalTurnCounts + "," +
                subjectLenaVarsInContact.totalChildUttCount + "," +
                partnerLenaVarsInContact.totalChildUttCount + "," +
                subjectLenaVarsInContact.totalAdultWordCount + "," +
                partnerLenaVarsInContact.totalAdultWordCount + "," +
                subjectLenaVarsInContact.totalNoise + "," +
                partnerLenaVarsInContact.totalNoise + "," +
                subjectLenaVarsInContact.totalOLN + "," +
                partnerLenaVarsInContact.totalOLN + "," +
                subjectLenaVarsInContact.totalChildCryDuration + "," +
                partnerLenaVarsInContact.totalChildCryDuration + "," +

                pair.joinedCry + "," +
                pair.subjectJoinedCry + "," +
                pair.partnerJoinedCry + "," +
                pair.pairProxDuration + "," +
                pair.pairProxOriDuration + "," +
                pair.sharedTimeInSecs + "," +
                pair.subjectTotalTimeInSecs + "," +
                pair.partnerTotalTimeInSecs + "," +
                recSecs + "," +
                subjectLenaVarsInWUBI.totalChildUttDuration + "," +
                subjectLenaVarsInTotal.totalChildUttDuration + "," +
                partnerLenaVarsInWUBI.totalChildUttDuration + "," +
                partnerLenaVarsInTotal.totalChildUttDuration + "," +

                subjectLenaVarsInWUBI.totalChildUttCount + "," +
                subjectLenaVarsInTotal.totalChildUttCount + "," +
                partnerLenaVarsInWUBI.totalChildUttCount + "," +
                partnerLenaVarsInTotal.totalChildUttCount + "," +
                subjectLenaVarsInWUBI.totalTurnCounts + "," +
                subjectLenaVarsInTotal.totalTurnCounts + "," +
                partnerLenaVarsInWUBI.totalTurnCounts + "," +
                partnerLenaVarsInTotal.totalTurnCounts + "," +

                subjectLenaVarsInWUBI.totalAdultWordCount + "," +
                subjectLenaVarsInTotal.totalAdultWordCount + "," +
                partnerLenaVarsInWUBI.totalAdultWordCount + "," +
                partnerLenaVarsInTotal.totalAdultWordCount + "," +

                subjectLenaVarsInWUBI.totalNoise + "," +
                subjectLenaVarsInTotal.totalNoise + "," +
                partnerLenaVarsInWUBI.totalNoise + "," +
                partnerLenaVarsInTotal.totalNoise + "," +

                subjectLenaVarsInWUBI.totalOLN + "," +
                subjectLenaVarsInTotal.totalOLN + "," +
                partnerLenaVarsInWUBI.totalOLN + "," +
                partnerLenaVarsInTotal.totalOLN + "," +

                subjectLenaVarsInWUBI.totalChildCryDuration + "," +
                subjectLenaVarsInTotal.totalChildCryDuration + "," +
                partnerLenaVarsInWUBI.totalChildCryDuration + "," +
                partnerLenaVarsInTotal.totalChildCryDuration + "," +


                (subjectLenaVarsInWUBI.avgDb != 0 && subjectLenaVarsInWUBI.totalSegments != 0 ? subjectLenaVarsInWUBI.avgDb / subjectLenaVarsInWUBI.totalSegments : 0.00).ToString() + "," +
                (subjectLenaVarsInTotal.avgDb != 0 && subjectLenaVarsInTotal.totalSegments != 0 ? subjectLenaVarsInTotal.avgDb / subjectLenaVarsInTotal.totalSegments : 0.00).ToString() + "," +
                (partnerLenaVarsInWUBI.avgDb != 0 && partnerLenaVarsInWUBI.totalSegments != 0 ? partnerLenaVarsInWUBI.avgDb / partnerLenaVarsInWUBI.totalSegments : 0.00).ToString() + "," +
                (partnerLenaVarsInTotal.avgDb != 0 && partnerLenaVarsInTotal.totalSegments != 0 ? partnerLenaVarsInTotal.avgDb / partnerLenaVarsInTotal.totalSegments : 0.00).ToString() + "," +

                (subjectLenaVarsInWUBI.maxDb != 0 && subjectLenaVarsInWUBI.totalSegments != 0 ? subjectLenaVarsInWUBI.maxDb / subjectLenaVarsInWUBI.totalSegments : 0.00).ToString() + "," +
                (subjectLenaVarsInTotal.maxDb != 0 && subjectLenaVarsInTotal.totalSegments != 0 ? subjectLenaVarsInTotal.maxDb / subjectLenaVarsInTotal.totalSegments : 0.00).ToString() + "," +
                (partnerLenaVarsInWUBI.maxDb != 0 && partnerLenaVarsInWUBI.totalSegments != 0 ? partnerLenaVarsInWUBI.maxDb / partnerLenaVarsInWUBI.totalSegments : 0.00).ToString() + "," +
                (partnerLenaVarsInTotal.maxDb != 0 && partnerLenaVarsInTotal.totalSegments != 0 ? partnerLenaVarsInTotal.maxDb / partnerLenaVarsInTotal.totalSegments : 0.00).ToString() + "," +
                className);







                szSubject = szPair.Split('|')[1];
                szPartner = szPair.Split('|')[0];
                subject = personBaseMappings[szSubject];
                partner = personBaseMappings[szPartner];
                sdi = Utilities.getPerson(personDayMappings, szSubject);
                pdi = Utilities.getPerson(personDayMappings, szPartner);

                subjectLenaVarsInContact = pair.partnerLenaVarsInContact;
                partnerLenaVarsInContact = pair.subjectLenaVarsInContact;
                 


                subjectLenaVarsInWUBI = personDayMappings[szPartner].WUBILenaVars;
                partnerLenaVarsInWUBI = personDayMappings[szSubject].WUBILenaVars;
                subjectLenaVarsInTotal = personDayMappings[szPartner].totalLenaVars;
                partnerLenaVarsInTotal = personDayMappings[szSubject].totalLenaVars;

                sw.WriteLine(this.classDay.Month + "/" + this.classDay.Day + "/" + this.classDay.Year + "," +
               subject.longId + "," +
               partner.longId + "," +
               subject.shortId + "," +
               partner.shortId + "," +
                newPairDiagnosisP +
                newPairLanguagesP +
               subject.gender + "," +
               partner.gender + "," +
               (subject.longId.Contains("_L") || subject.longId.Contains("_T") || partner.longId.Contains("_L") || partner.longId.Contains("_T")) + "," +
               sdi.status + "," +
               pdi.status + "," +
               subject.subjectType + "," +
               partner.subjectType + "," +
               (sdi.status.ToUpper() == "PRESENT" ? (subject.subjectType.ToUpper() == "CHILD" ? partnerLenaVarsInContact.totalChildUttCount.ToString() : subjectLenaVarsInContact.totalAdultWordCount.ToString()) : "NA") + "," +
               (sdi.status.ToUpper() == "PRESENT" ? (subject.subjectType.ToUpper() == "CHILD" ? partnerLenaVarsInContact.totalChildUttCount.ToString() : subjectLenaVarsInContact.totalChildUttCount.ToString()) : "NA") + "," +
               (sdi.status.ToUpper() == "PRESENT" ? (subject.subjectType.ToUpper() == "CHILD" ? partnerLenaVarsInContact.totalChildUttDuration.ToString() : subjectLenaVarsInContact.totalAdultWordCount.ToString()) : "NA") + "," +
               pair.pairBlockTalking + "," +
               pair.pairProxOriDuration + "," +//partnerLenaVarsInContact.totalChildUttDuration + "," +
//"NA," +
//"NA," +
               subjectLenaVarsInContact.totalChildUttDuration + "," +
               partnerLenaVarsInContact.totalChildUttDuration + "," +
               subjectLenaVarsInContact.totalTurnCounts + "," +
               partnerLenaVarsInContact.totalTurnCounts + "," +
               subjectLenaVarsInContact.totalChildUttCount + "," +
               partnerLenaVarsInContact.totalChildUttCount + "," +
               subjectLenaVarsInContact.totalAdultWordCount + "," +
               partnerLenaVarsInContact.totalAdultWordCount + "," +
               subjectLenaVarsInContact.totalNoise + "," +
               partnerLenaVarsInContact.totalNoise + "," +
               subjectLenaVarsInContact.totalOLN + "," +
               partnerLenaVarsInContact.totalOLN + "," +
               subjectLenaVarsInContact.totalChildCryDuration + "," +
               partnerLenaVarsInContact.totalChildCryDuration + "," +

               pair.joinedCry + "," +
               pair.partnerJoinedCry + "," +
               pair.subjectJoinedCry + "," +
               pair.pairProxDuration + "," +
               pair.pairProxOriDuration + "," +
               pair.sharedTimeInSecs + "," +
               pair.partnerTotalTimeInSecs + "," +
               pair.subjectTotalTimeInSecs + "," +


               recSecs + "," +
               subjectLenaVarsInWUBI.totalChildUttDuration + "," +
               subjectLenaVarsInTotal.totalChildUttDuration + "," +
               partnerLenaVarsInWUBI.totalChildUttDuration + "," +
               partnerLenaVarsInTotal.totalChildUttDuration + "," +

               subjectLenaVarsInWUBI.totalChildUttCount + "," +
               subjectLenaVarsInTotal.totalChildUttCount + "," +
               partnerLenaVarsInWUBI.totalChildUttCount + "," +
               partnerLenaVarsInTotal.totalChildUttCount + "," +
               subjectLenaVarsInWUBI.totalTurnCounts + "," +
               subjectLenaVarsInTotal.totalTurnCounts + "," +
               partnerLenaVarsInWUBI.totalTurnCounts + "," +
               partnerLenaVarsInTotal.totalTurnCounts + "," +

               subjectLenaVarsInWUBI.totalAdultWordCount + "," +
               subjectLenaVarsInTotal.totalAdultWordCount + "," +
               partnerLenaVarsInWUBI.totalAdultWordCount + "," +
               partnerLenaVarsInTotal.totalAdultWordCount + "," +

               subjectLenaVarsInWUBI.totalNoise + "," +
               subjectLenaVarsInTotal.totalNoise + "," +
               partnerLenaVarsInWUBI.totalNoise + "," +
               partnerLenaVarsInTotal.totalNoise + "," +

               subjectLenaVarsInWUBI.totalOLN + "," +
               subjectLenaVarsInTotal.totalOLN + "," +
               partnerLenaVarsInWUBI.totalOLN + "," +
               partnerLenaVarsInTotal.totalOLN + "," +

               subjectLenaVarsInWUBI.totalChildCryDuration + "," +
               subjectLenaVarsInTotal.totalChildCryDuration + "," +
               partnerLenaVarsInWUBI.totalChildCryDuration + "," +
               partnerLenaVarsInTotal.totalChildCryDuration + "," +


               (subjectLenaVarsInWUBI.avgDb != 0 && subjectLenaVarsInWUBI.totalSegments != 0 ? subjectLenaVarsInWUBI.avgDb / subjectLenaVarsInWUBI.totalSegments : 0.00).ToString() + "," +
               (subjectLenaVarsInTotal.avgDb != 0 && subjectLenaVarsInTotal.totalSegments != 0 ? subjectLenaVarsInTotal.avgDb / subjectLenaVarsInTotal.totalSegments : 0.00).ToString() + "," +
               (partnerLenaVarsInWUBI.avgDb != 0 && partnerLenaVarsInWUBI.totalSegments != 0 ? partnerLenaVarsInWUBI.avgDb / partnerLenaVarsInWUBI.totalSegments : 0.00).ToString() + "," +
               (partnerLenaVarsInTotal.avgDb != 0 && partnerLenaVarsInTotal.totalSegments != 0 ? partnerLenaVarsInTotal.avgDb / partnerLenaVarsInTotal.totalSegments : 0.00).ToString() + "," +

               (subjectLenaVarsInWUBI.maxDb != 0 && subjectLenaVarsInWUBI.totalSegments != 0 ? subjectLenaVarsInWUBI.maxDb / subjectLenaVarsInWUBI.totalSegments : 0.00).ToString() + "," +
               (subjectLenaVarsInTotal.maxDb != 0 && subjectLenaVarsInTotal.totalSegments != 0 ? subjectLenaVarsInTotal.maxDb / subjectLenaVarsInTotal.totalSegments : 0.00).ToString() + "," +
               (partnerLenaVarsInWUBI.maxDb != 0 && partnerLenaVarsInWUBI.totalSegments != 0 ? partnerLenaVarsInWUBI.maxDb / partnerLenaVarsInWUBI.totalSegments : 0.00).ToString() + "," +
               (partnerLenaVarsInTotal.maxDb != 0 && partnerLenaVarsInTotal.totalSegments != 0 ? partnerLenaVarsInTotal.maxDb / partnerLenaVarsInTotal.totalSegments : 0.00).ToString() + "," +
               className);


            }
            sw.Close();
        }
        public Boolean hasAllInfo(PersonSuperInfo p1)
        {
            return ((!double.IsNaN(p1.x)) &&
                    (!double.IsNaN(p1.y)) &&
                    (!double.IsNaN(p1.xl)) &&
                    (!double.IsNaN(p1.yl)) &&
                    (!double.IsNaN(p1.xr)) &&
                    (!double.IsNaN(p1.yr)) &&
                    p1.x!=0 &&
                    p1.y!=0);
        }
       
        public Dictionary<String, Pair> countInteractions(double minGr, double maxGr, double angle, String szAngleOutputFile, String szAppOutputFile)
        {//pairs are unique not repeated
            Dictionary<String, Pair> pairs = Utilities.getSzPairKey(personDayMappings);
            Dictionary<String, int> onsetPos = new Dictionary<string, int>();
            Boolean doAngles = szAngleOutputFile != "";
            Boolean doApp = szAppOutputFile != "";

            TextWriter sw = doAngles? new StreamWriter(szAngleOutputFile):null;
            TextWriter swapp =doApp? new StreamWriter(szAppOutputFile):null;
            if(doAngles) 
            sw.WriteLine("Person 1, Person2, Interaction Time, Interaction Millisecond, Interaction, "+ angle+"Interaction, Angle1, Angle2, Leftx,Lefty,Rightx,Righty, Leftx2,Lefty2,Rightx2,Righty2,Type1, Type2, Gender1, Gender2, Diagnosis1, Diagnosis2, WasTalking1, WasTalking2 ");

            if (doApp)
            swapp.WriteLine("Person 1, Person2, Interaction Time, Interaction Millisecond,d1,d2,approachMeters,x10,y10,x20,y20,x11,y11,x21,y21, WithinGR, WithinGRAnd" + angle + "deg, Angle1, Angle2,Type1, Type2, Gender1, Gender2, Diagnosis1, Diagnosis2,LongPerson 1, LongPerson2,  ");

            foreach (DateTime t in ubiTenths.Keys)
            {
                foreach (String szPairKey in pairs.Keys)
                {
                    Pair pair = pairs[szPairKey];
                    if (ubiTenths[t].ContainsKey(pair.szSubjectMapId))
                    {
                        pair.subjectTotalTimeInSecs += .1;

                    }
                    if (ubiTenths[t].ContainsKey(pair.szPartnerMapId))
                    {
                        pair.partnerTotalTimeInSecs += .1;
                    }

                    if (ubiTenths[t].ContainsKey(pair.szSubjectMapId) && 
                        ubiTenths[t].ContainsKey(pair.szPartnerMapId) &&
                        hasAllInfo(ubiTenths[t][pair.szSubjectMapId]) &&
                        hasAllInfo(ubiTenths[t][pair.szPartnerMapId]))
                    {
                        pair.sharedTimeInSecs += .1;
                        double dist = Utilities.calcSquaredDist(ubiTenths[t][pair.szSubjectMapId], ubiTenths[t][pair.szPartnerMapId]);
                        Boolean withinGofR = (dist <= (maxGr * maxGr)) && (dist >= (minGr * minGr));
                        Tuple<double, double> angles = Utilities.withinOrientationData(ubiTenths[t][pair.szSubjectMapId], ubiTenths[t][pair.szPartnerMapId]);
                        Boolean orientedCloseness = withinGofR && (Math.Abs(angles.Item1) <= angle && Math.Abs(angles.Item2) <= angle);
                        //
                        if(doAngles)
                        sw.WriteLine(pair.szSubjectMapId + "," +
                                                                        pair.szPartnerMapId + "," +
                                                                        t.ToLongTimeString() + "," +
                                                                        t.Millisecond + "," +
                                                                        (withinGofR ? "0.1" : "0") + "," +
                                                                        (orientedCloseness ? "0.1" : "0") + "," +
                                                                        (angles.Item1) + "," +
                                                                        (angles.Item2) + "," +
                                                                        ubiTenths[t][pair.szSubjectMapId].xl + "," +
                                                                        ubiTenths[t][pair.szSubjectMapId].yl + "," +
                                                                        ubiTenths[t][pair.szSubjectMapId].xr + "," +
                                                                        ubiTenths[t][pair.szSubjectMapId].yr + "," +
                                                                        ubiTenths[t][pair.szPartnerMapId].xl + "," +
                                                                        ubiTenths[t][pair.szPartnerMapId].yl + "," +
                                                                        ubiTenths[t][pair.szPartnerMapId].xr + "," +
                                                                        ubiTenths[t][pair.szPartnerMapId].yr + "," +
                                                                    personBaseMappings[pair.szSubjectMapId].subjectType + "," +
                                                                    personBaseMappings[pair.szPartnerMapId].subjectType + "," +
                                                                    personBaseMappings[pair.szSubjectMapId].gender + "," +
                                                                    personBaseMappings[pair.szPartnerMapId].gender + "," +
                                                                    personBaseMappings[pair.szSubjectMapId].diagnosis + "," +
                                                                    personBaseMappings[pair.szPartnerMapId].diagnosis + "," +
                                                                    ubiTenths[t][pair.szSubjectMapId].wasTalking + "," +
                                                                    ubiTenths[t][pair.szPartnerMapId].wasTalking);


                        
                         
                        pair.pairProxDuration += (withinGofR?.1:0);
                        if (withinGofR && orientedCloseness)
                        {
                            pair.pairBlockTalking += (ubiTenths[t][pair.szSubjectMapId].lenaVars.totalChildUttDuration + ubiTenths[t][pair.szPartnerMapId].lenaVars.totalChildUttDuration);

                            pair.pairProxOriDuration += .1;

                            pair.subjectLenaVarsInContact.totalTurnCounts += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalTurnCounts;
                            pair.subjectLenaVarsInContact.totalChildUttCount += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalChildUttCount;
                            pair.subjectLenaVarsInContact.totalChildUttDuration += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalChildUttDuration;
                            pair.subjectLenaVarsInContact.totalChildCryDuration += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalChildCryDuration;
                            pair.subjectLenaVarsInContact.totalAdultWordCount += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalAdultWordCount;
                            pair.subjectLenaVarsInContact.totalNoise += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalNoise;
                            pair.subjectLenaVarsInContact.totalOLN += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalOLN;


                            pair.partnerLenaVarsInContact.totalTurnCounts += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalTurnCounts;
                            pair.partnerLenaVarsInContact.totalChildUttCount += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalChildUttCount;
                            pair.partnerLenaVarsInContact.totalChildUttDuration += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalChildUttDuration;
                            pair.partnerLenaVarsInContact.totalChildCryDuration += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalChildCryDuration;
                            pair.partnerLenaVarsInContact.totalAdultWordCount += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalAdultWordCount;
                            pair.partnerLenaVarsInContact.totalNoise += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalNoise;
                            pair.partnerLenaVarsInContact.totalOLN += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalOLN;
                             
                            if (ubiTenths[t][pair.szSubjectMapId].lenaVars.totalChildCryDuration > 0.00 && ubiTenths[t][pair.szPartnerMapId].lenaVars.totalChildCryDuration>0.00)
                            {
                                pair.joinedCry+= (ubiTenths[t][pair.szSubjectMapId].lenaVars.totalChildCryDuration + ubiTenths[t][pair.szPartnerMapId].lenaVars.totalChildCryDuration);
                                pair.subjectJoinedCry += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalChildCryDuration;
                                pair.partnerJoinedCry += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalChildCryDuration;

                            }
                            //FOR SOCIAL ONSETS
                            if(ubiTenths[t][pair.szSubjectMapId].wasTalking || ubiTenths[t][pair.szPartnerMapId].wasTalking)
                            {
                                if (lenaOnsets.ContainsKey(pair.szSubjectMapId))
                                {
                                    int sPos = 0;
                                    if (!onsetPos.ContainsKey(pair.szSubjectMapId))
                                        onsetPos.Add(pair.szSubjectMapId, 0);
                                    else
                                        sPos = onsetPos[pair.szSubjectMapId];

                                    for (; sPos < lenaOnsets[pair.szSubjectMapId].Count; sPos++)
                                    {
                                        if (t.CompareTo(lenaOnsets[pair.szSubjectMapId][sPos].startTime) < 0)
                                            break;
                                        if (t.CompareTo(lenaOnsets[pair.szSubjectMapId][sPos].startTime) >= 0 &&
                                            t.CompareTo(lenaOnsets[pair.szSubjectMapId][sPos].endTime) <= 0)
                                        {
                                            lenaOnsets[pair.szSubjectMapId][sPos].inSocialContact = true;

                                        }
                                    }
                                    onsetPos[pair.szSubjectMapId] = sPos;
                                }


                               if (lenaOnsets.ContainsKey(pair.szPartnerMapId))
                               { int pPos = 0;
                                    if (!onsetPos.ContainsKey(pair.szPartnerMapId))
                                        onsetPos.Add(pair.szPartnerMapId, 0);
                                    else
                                        pPos = onsetPos[pair.szPartnerMapId];

                                    for (; pPos < lenaOnsets[pair.szPartnerMapId].Count; pPos++)
                                    {
                                        if (t.CompareTo(lenaOnsets[pair.szPartnerMapId][pPos].startTime) < 0)
                                            break;
                                        if (t.CompareTo(lenaOnsets[pair.szPartnerMapId][pPos].startTime) >= 0 &&
                                            t.CompareTo(lenaOnsets[pair.szPartnerMapId][pPos].endTime) <= 0)
                                        {
                                            lenaOnsets[pair.szPartnerMapId][pPos].inSocialContact = true;

                                        }
                                    }
                                    onsetPos[pair.szPartnerMapId] = pPos;
                                }
                               
                                ///

                            }

                        }//insocialcontact


                        pair.subjectLenaVarsInWUBI.totalTurnCounts += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalTurnCounts;
                        pair.subjectLenaVarsInWUBI.totalChildUttCount += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalChildUttCount;
                        pair.subjectLenaVarsInWUBI.totalChildUttDuration += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalChildUttDuration;
                        pair.subjectLenaVarsInWUBI.totalChildCryDuration += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalChildCryDuration;
                        pair.subjectLenaVarsInWUBI.totalAdultWordCount += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalAdultWordCount;
                        pair.subjectLenaVarsInWUBI.totalNoise += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalNoise;
                        pair.subjectLenaVarsInWUBI.totalOLN += ubiTenths[t][pair.szSubjectMapId].lenaVars.totalOLN;


                        pair.partnerLenaVarsInWUBI.totalTurnCounts += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalTurnCounts;
                        pair.partnerLenaVarsInWUBI.totalChildUttCount += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalChildUttCount;
                        pair.partnerLenaVarsInWUBI.totalChildUttDuration += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalChildUttDuration;
                        pair.partnerLenaVarsInWUBI.totalChildCryDuration += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalChildCryDuration;
                        pair.partnerLenaVarsInWUBI.totalAdultWordCount += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalAdultWordCount;
                        pair.partnerLenaVarsInWUBI.totalNoise += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalNoise;
                        pair.partnerLenaVarsInWUBI.totalOLN += ubiTenths[t][pair.szPartnerMapId].lenaVars.totalOLN;

                        if (doApp)
                        {
                            double dist0 = 0;
                            double dist1 = 0;
                            DateTime dt0 = t.AddMilliseconds(-100);
                            double approachMetersS = 0;
                            double approachMetersP = 0;
                            if (ubiTenths.ContainsKey(dt0) && ubiTenths[dt0].ContainsKey(pair.szPartnerMapId) && ubiTenths[dt0].ContainsKey(pair.szSubjectMapId))
                            {
                                dist1 = Math.Sqrt(Utilities.calcSquaredDist(ubiTenths[t][pair.szSubjectMapId], ubiTenths[dt0][pair.szPartnerMapId]));
                                dist0 = Math.Sqrt(Utilities.calcSquaredDist(ubiTenths[dt0][pair.szSubjectMapId], ubiTenths[dt0][pair.szPartnerMapId]));
                                approachMetersS = dist0 - dist1;
                                if (!Double.IsNaN(approachMetersS))
                                {
                                    pair.subjectDistCount++;
                                    pair.subjectDist += dist0;
                                }

                                if (!Double.IsNaN(approachMetersS))
                                {

                                    ////
                                    String appLine =
                                        personBaseMappings[pair.szSubjectMapId].shortId + "," +
                                        personBaseMappings[pair.szPartnerMapId].shortId + "," +
                                        t.ToLongTimeString() + "," +
                                        t.Millisecond + "," +
                                        dist0 + "," +
                                        dist1 + "," +
                                        approachMetersS + "," +
                                        ubiTenths[dt0][pair.szSubjectMapId].x + "," +
                                        ubiTenths[dt0][pair.szSubjectMapId].y + "," +
                                        ubiTenths[dt0][pair.szPartnerMapId].x + "," +
                                        ubiTenths[dt0][pair.szPartnerMapId].y + "," +
                                        ubiTenths[t][pair.szSubjectMapId].x + "," +
                                        ubiTenths[t][pair.szSubjectMapId].y + "," +
                                        ubiTenths[t][pair.szPartnerMapId].x + "," +
                                        ubiTenths[t][pair.szPartnerMapId].y + "," +
                                        (withinGofR ? "TRUE" : "FALSE") + "," +
                                        (orientedCloseness ? "TRUE" : "FALSE") + "," +
                                        (angles.Item1) + "," +
                                        (angles.Item2) + "," +
                                        personBaseMappings[pair.szSubjectMapId].subjectType + "," +
                                        personBaseMappings[pair.szPartnerMapId].subjectType + "," +
                                        personBaseMappings[pair.szSubjectMapId].gender + "," +
                                        personBaseMappings[pair.szPartnerMapId].gender + "," +
                                        personBaseMappings[pair.szSubjectMapId].diagnosis + "," +
                                        personBaseMappings[pair.szPartnerMapId].diagnosis + "," +
                                        pair.szSubjectMapId + "," +
                                        pair.szPartnerMapId;


                                    swapp.WriteLine(appLine);
                                }


                                 dist1 = Math.Sqrt(Utilities.calcSquaredDist(ubiTenths[t][pair.szPartnerMapId], ubiTenths[dt0][pair.szSubjectMapId]));
                                dist0 = Math.Sqrt(Utilities.calcSquaredDist(ubiTenths[dt0][pair.szPartnerMapId], ubiTenths[dt0][pair.szSubjectMapId]));
                                approachMetersP = dist0 - dist1;
                                if (!Double.IsNaN(approachMetersP))
                                {
                                    pair.partnerDistCount++;
                                    pair.partnerDist += dist0;
                                }

                                if (!Double.IsNaN(approachMetersP))
                                {

                                    ////   szSubjectMapId      szPartnerMapId
                                    String appLine =
                                        personBaseMappings[pair.szPartnerMapId].shortId + "," +
                                        personBaseMappings[pair.szSubjectMapId].shortId + "," +
                                        t.ToLongTimeString() + "," +
                                        t.Millisecond + "," +
                                        dist0 + "," +
                                        dist1 + "," +
                                        approachMetersS + "," +
                                        ubiTenths[dt0][pair.szPartnerMapId].x + "," +
                                        ubiTenths[dt0][pair.szPartnerMapId].y + "," +
                                        ubiTenths[dt0][pair.szSubjectMapId].x + "," +
                                        ubiTenths[dt0][pair.szSubjectMapId].y + "," +
                                        ubiTenths[t][pair.szPartnerMapId].x + "," +
                                        ubiTenths[t][pair.szPartnerMapId].y + "," +
                                        ubiTenths[t][pair.szSubjectMapId].x + "," +
                                        ubiTenths[t][pair.szSubjectMapId].y + "," +
                                        (withinGofR ? "TRUE" : "FALSE") + "," +
                                        (orientedCloseness ? "TRUE" : "FALSE") + "," +
                                        (angles.Item1) + "," +
                                        (angles.Item2) + "," +
                                        personBaseMappings[pair.szPartnerMapId].subjectType + "," +
                                        personBaseMappings[pair.szSubjectMapId].subjectType + "," +
                                        personBaseMappings[pair.szPartnerMapId].gender + "," +
                                        personBaseMappings[pair.szSubjectMapId].gender + "," +
                                        personBaseMappings[pair.szPartnerMapId].diagnosis + "," +
                                        personBaseMappings[pair.szSubjectMapId].diagnosis + "," +
                                        pair.szPartnerMapId + "," +
                                        pair.szSubjectMapId;


                                    swapp.WriteLine(appLine);
                                }

                            }
                        }



                    }
                }
            }
            if(doAngles)
            sw.Close();
            if(doApp)
            swapp.Close();
            //Date	Subject	Partner	SubjectShortID	PartnerShortID	SubjectDiagnosis	PartnerDiagnosis	
            //SubjectGender	PartnerGender	SubjectLanguage	PartnerLanguage	Adult	SubjectStatus	PartnerStatus	SubjectType	PartnerType
            return pairs;


        }
            public void setMappings(String mappingDayFileName, Dictionary<String, Person> personMappings, String mapById, int startHour, int endHour, int endMinute)
            {
                personBaseMappings = personMappings;
                if (File.Exists(mappingDayFileName))
                using (StreamReader sr = new StreamReader(mappingDayFileName))
                {
                    if (!sr.EndOfStream)
                    {
                        sr.ReadLine();
                    }

                    while ((!sr.EndOfStream))// && lineCount < 10000)
                    {
                        String commaLine = sr.ReadLine();
                        String[] line = commaLine.Split(',');
                        if (line.Length > 16 && line[1] != "")
                        {
                            Person person = new Person(commaLine, mapById, new List<int>(), new List<int>());
                            if (personMappings.ContainsKey(person.mapId))
                            {
                                person = personMappings[person.mapId];
                                PersonDayInfo personDayInfo = new PersonDayInfo(commaLine, person.mapId, new DateTime(classDay.Year, classDay.Month, classDay.Day, startHour, 0, 0), new DateTime(classDay.Year, classDay.Month, classDay.Day, endHour, endMinute, 0));
                                if (!personDayMappings.ContainsKey(person.mapId))
                                personDayMappings.Add(person.mapId, personDayInfo);
                            }
                        }
                    }
                }

            }
            public void findTagPerson(ref UbiLocation ubiLocation, DateTime dt)
            {
                foreach(String key in personDayMappings.Keys)
                {
                    PersonDayInfo pdi = personDayMappings[key];
                    if(pdi.present && pdi.status=="PRESENT" &&
                        dt>=pdi.startDate && dt<=pdi.endDate)
                    {
                        if (ubiLocation.tag == pdi.leftUbi)
                        {
                            ubiLocation.id = key;
                            ubiLocation.type = "L";
                            break;
                        }
                        else if (ubiLocation.tag == pdi.rightUbi)
                        {
                            ubiLocation.id = key;
                            ubiLocation.type = "R";
                            break;
                        }


                    }

                }

            }

            public void readUbiLogsFromGrFile(String dir) 
            {
                String szDayFolder = Utilities.getDateDashStr(classDay);
                

                string[] ubiLogFiles = Directory.GetFiles(dir + "//" + szDayFolder + "//Ubisense_Data//");
                foreach (string file in ubiLogFiles)
                {
                    String fileName = Path.GetFileName(file);
                    if (fileName.StartsWith("MiamiLocation") && fileName.EndsWith(".log"))
                    {

                        using (StreamReader sr = new StreamReader(file))
                        {
                            while (!sr.EndOfStream)
                            {
                                String szLine = sr.ReadLine();
                                String[] line = szLine.Split(',');
                                if (line.Length > 5)
                                {

                                    UbiLocation ubiLoc = new UbiLocation();
                                    ubiLoc.tag = line[9].Trim(); 
                                    String ubiId = line[1].Trim();
                                    String ubiType = ubiId.Substring(ubiId.Length - 1);
                                    ubiId= ubiId.Substring(0,ubiId.Length - 1);
                                    ubiLoc.id = ubiId;
                                    DateTime lineTime = Convert.ToDateTime(line[2]);
                                    Double xPos = Convert.ToDouble(line[3]);
                                    Double yPos = Convert.ToDouble(line[4]);
                                    ubiLoc.x = xPos;
                                    ubiLoc.y = yPos;
                                    ubiLoc.time = lineTime;
                                    if (ubiLoc.type == "L")
                                    {
                                        if (!ubiLocationsL.ContainsKey(ubiLoc.id))
                                            ubiLocationsL.Add(ubiLoc.id, new List<UbiLocation>());
                                        ubiLocationsL[ubiLoc.id].Add(ubiLoc);
                                    }
                                    else
                                    {
                                        if (!ubiLocationsR.ContainsKey(ubiLoc.id))
                                            ubiLocationsR.Add(ubiLoc.id, new List<UbiLocation>());
                                        ubiLocationsR[ubiLoc.id].Add(ubiLoc);
                                    }
                                     
                                }
                            }
                        }
                    }
                } 
            }

        public void readUbiLogsAndWriteGrFile(String dir, String szOutputFile, int startHour, int endHour)//, ref ClassroomDay classroomDay)
        {
            String szDayFolder = Utilities.getDateDashStr(classDay);
            TextWriter sw = new StreamWriter(szOutputFile);// countDays > 0);


            string[] ubiLogFiles = Directory.GetFiles(dir + "//" + szDayFolder + "//Ubisense_Data//");
            foreach (string file in ubiLogFiles)
            {
                String fileName = Path.GetFileName(file);
                if (fileName.StartsWith("MiamiLocation") && fileName.EndsWith(".log"))
                {

                    using (StreamReader sr = new StreamReader(file))
                    {
                        while (!sr.EndOfStream)
                        {
                            String szLine = sr.ReadLine();
                            String[] line = szLine.Split(',');
                            if (line.Length >= 5)
                            {
                                String tag = line[1].Trim();
                                DateTime lineTime = Convert.ToDateTime(line[2]);
                                Double xPos = Convert.ToDouble(line[3]);
                                Double yPos = Convert.ToDouble(line[4]);
                                if (Utilities.isSameDay(lineTime, classDay) &&
                                    lineTime.Hour >= startHour &&
                                    lineTime.Hour <= endHour)
                                {
                                    UbiLocation ubiLoc = new UbiLocation();
                                    ubiLoc.tag = tag;
                                    findTagPerson(ref ubiLoc, lineTime);



                                    if (ubiLoc.id != "")
                                    {
                                        ubiLoc.x = xPos;
                                        ubiLoc.y = yPos;
                                        ubiLoc.time = lineTime;
                                        if (ubiLoc.type == "L")
                                        {
                                            if (!ubiLocationsL.ContainsKey(ubiLoc.id))
                                                ubiLocationsL.Add(ubiLoc.id, new List<UbiLocation>());
                                            ubiLocationsL[ubiLoc.id].Add(ubiLoc);
                                        }
                                        else
                                        {
                                            if (!ubiLocationsR.ContainsKey(ubiLoc.id))
                                                ubiLocationsR.Add(ubiLoc.id, new List<UbiLocation>());
                                            ubiLocationsR[ubiLoc.id].Add(ubiLoc);
                                        }

                                        sw.WriteLine(szLine.Replace(tag, ubiLoc.id + ubiLoc.type) +"," + ubiLoc.id + ubiLoc.type+","+ tag);
                                        // swc.WriteLine(szLine.Replace(i.tag, personId + i.tagType) + "," + i.tag);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            sw.Close();
        }

        public void createCleanUbiFile(String dir, int startHour, int endHour)//, ref ClassroomDay classroomDay)
        {
            String szDayFolder = Utilities.getDateDashStr(classDay);
            String szDenoisedFolder = dir + "//" + szDayFolder + "//Ubisense_Filtered_Data";

            if (!Directory.Exists(szDenoisedFolder))
                Directory.CreateDirectory(szDenoisedFolder);

            

            string[] ubiLogFiles = Directory.GetFiles(dir + "//" + szDayFolder + "//Ubisense_Data//");
            foreach (string file in ubiLogFiles)
            {
                String fileName = Path.GetFileName(file);
                if (fileName.StartsWith("MiamiLocation") && fileName.EndsWith(".log"))
                {
                    TextWriter sw = new StreamWriter(szDenoisedFolder+"//"+fileName.Replace(".log", "_filtered.log"));// countDays > 0);
                    using (StreamReader sr = new StreamReader(file))
                    {
                        while (!sr.EndOfStream)
                        {
                            String szLine = sr.ReadLine();
                            String[] line = szLine.Split(',');
                            if (line.Length >= 5)
                            {
                                String tag = line[1].Trim();
                                DateTime lineTime = Convert.ToDateTime(line[2]);
                                Double xPos = Convert.ToDouble(line[3]);
                                Double yPos = Convert.ToDouble(line[4]);
                                if (Utilities.isSameDay(lineTime, classDay) &&
                                    lineTime.Hour >= startHour &&
                                    lineTime.Hour <= endHour)
                                {
                                    UbiLocation ubiLoc = new UbiLocation();
                                    ubiLoc.tag = tag;
                                    findTagPerson(ref ubiLoc, lineTime);



                                    if (ubiLoc.id != "")
                                    {
                                        sw.WriteLine(szLine);
                                    }
                                }
                            }
                        }
                        sw.Close();
                    }
                }
            }
            
        }

        public void setUbiTagData()
                    {

                    }
                    public Tuple<String,String,String, String, String> getLogActivities(DateTime start, DateTime end, Boolean ifPresent, String subject)
                    {
                        String acts = "";
                        String children = "";
                        String teachers  = "";
                        String nchildren = "";
                        String nteachers = "";
                        start = start.AddSeconds(-start.Second).AddMilliseconds(-start.Millisecond);
                        end = end.AddSeconds(-end.Second).AddMilliseconds(-end.Millisecond);
                        //DELETE DEBUG
                        if (start.Hour>=9&&start.Minute>=7)
                        {
                            bool stop = true;
                        }
                        foreach(Activity act in logActivities)
                        {

                            //Boolean dateWithin = act.start.TimeOfDay>=start.TimeOfDay && act.start.TimeOfDay<=end.TimeOfDay;
                            //dateWithin = dateWithin || act.end.TimeOfDay >= start.TimeOfDay && act.end.TimeOfDay <= end.TimeOfDay;

                            Boolean dateWithin = (start >= act.start && start <= act.end) || (end >= act.start && end <= act.end);

                            //if ((act.start.TimeOfDay >= start.TimeOfDay && act.start.TimeOfDay <= end.TimeOfDay) ||
                            //    (act.end.TimeOfDay >= start.TimeOfDay && act.end.TimeOfDay <= end.TimeOfDay))
                            if (dateWithin)
                            {// getChildIdFromSzNumber(Dictionary<String, PersonDayInfo> personDayMappings, String szNum)

                                String szChildren = "";
                                String szTeachers = "";
                                int iChildren =0;
                                int iTeachers = 0;
                                Boolean present = ifPresent? false:true;
                                foreach (String szChildNum in act.szChildren.Split('|'))
                                {
                                    if(szChildNum == subject)
                                    present = true;
                                    iChildren++;
                                    szChildren = szChildren + (szChildren != "" ? "|" : "") + Utilities.getChildIdFromSzNumber(personDayMappings, szChildNum);
                                }
                                foreach (String szTNum in act.szTeachers.Split('|'))
                                {
                                    if (szTNum == subject)
                                    present = true;
                                    iTeachers++;
                                    szTeachers = szTeachers + (szTeachers != "" ? "|" : "") + Utilities.getTeacherIdFromSzNumber(personDayMappings, szTNum);
                                }
                                if (present)
                                {
                                    acts = acts != "" ? acts + "+" + act.type : act.type;
                                    nchildren = nchildren != "" ? nchildren + "+" + iChildren.ToString() : iChildren.ToString();
                                    nteachers = nteachers != "" ? nteachers + "+" + iTeachers.ToString() : iTeachers.ToString();
                                    children = children != "" ? children + "+" + szChildren : szChildren;
                                    teachers = teachers != "" ? teachers + "+" + szTeachers : szTeachers;
                                }
                            }
                            if (act.start.TimeOfDay > end.TimeOfDay)
                                break;
                        }

                        //DELETE DEBUG
                        if (start.Hour >= 9 && start.Minute >= 6 && acts=="")
                        {
                            bool stop = true;
                        }

                        return new Tuple<string, string, string, String, String>(acts,children,teachers,nchildren,nteachers);
                    }
        public void readOnsetsAndActivity(String dir, String szOutputFile, int startHour, int endHour, int endMinute)//, ref ClassroomDay classroomDay)
        {
            String szDayFolder = Utilities.getDateDashStr(classDay);
            TextWriter sw = new StreamWriter(szOutputFile);// countDays > 0);
            sw.WriteLine("Class (School_Class_AM/PM),Date (DD/MM/YY),Children_Included,Teachers_Included,Start Time (HH:MM),End Time (HH:MM),Number of Children,Number of Adults,Activity,AcLog_Comments,,,Questions");//,children,teachers");

            string[] szLenaItsFiles = Directory.GetFiles(dir + "//" + szDayFolder + "//LENA_Data//ITS//");
            foreach (Activity act in logActivities)
            {
                foreach (String szBubject in act.szChildren.Split('|'))
                {
                  
                    foreach (String szLenaSubject in lenaOnsets.Keys)
                    {
                        if(lenaOnsets[szLenaSubject].Count>0)
                        {
                            LenaOnset lo = lenaOnsets[szLenaSubject][0];
                           // if(lo.id=="")
                        }
                       // LenaOnset lo = lenaOnsets[szLenaSubject];
                    }

                }
                DateTime currentOnset = new DateTime(2000, 1, 1);
                  

            }
            sw.Close();
        }
                    public void readLenaItsAndGetOnsets(String dir,String szOutputFile, int startHour,int endHour, int endMinute )//, ref ClassroomDay classroomDay)
                    {
                        String szDayFolder = Utilities.getDateDashStr(classDay);
                        TextWriter sw = new StreamWriter(szOutputFile);// countDays > 0);
                        sw.WriteLine("File,Date,Subject,LenaID,SubjectType,segmentid," +
                            "voctype,recstart,startsec,endsec,starttime,endtime,duration," +
                            "seg_duration,wordcount,avg_db,avg_peak,turn_taking," +
                            "logActivities,children,teachers");//,children,teachers");


                        string[] szLenaItsFiles = Directory.GetFiles(dir + "//"+szDayFolder+ "//LENA_Data//ITS//");
                        foreach (string itsFile in szLenaItsFiles)
                        {
                            String szLenaId = Utilities.getLenaIdFromFileName(Path.GetFileName(itsFile));
                            XmlDocument doc = new XmlDocument();
                            doc.Load(itsFile);
                            XmlNodeList rec = doc.ChildNodes[0].SelectNodes("ProcessingUnit/Recording");
                            foreach (XmlNode recording in rec)
                            {
                                //DELETE DEBUG
                                if(szLenaId== "29640")
                                {
                                    Boolean stop = true;
                                }
                                double recStartSecs = Convert.ToDouble(recording.Attributes["startTime"].Value.Substring(2, recording.Attributes["startTime"].Value.Length - 3));
                                DateTime recStartTime = DateTime.Parse(recording.Attributes["startClockTime"].Value);
                                XmlNodeList nodes = recording.SelectNodes("Conversation|Pause");
                                PersonDayInfo pdi = getPersonInfoByLena(szLenaId);
                                if (pdi.mapId != "")
                                {
                                    Person pi = personBaseMappings[pdi.mapId];

                                    double segmentNumber = 0;

                                    if (    Utilities.isSameDay(recStartTime, classDay) &&
                                            recStartTime.Hour >= startHour &&
                                            (   recStartTime.Hour < endHour ||
                                                (   recStartTime.Hour == endHour && 
                                                    recStartTime.Minute <= endMinute
                                                )
                                            )
                                        )
                                        foreach (XmlNode conv in nodes)
                                        {
                                            XmlNodeList segments = conv.SelectNodes("Segment");
                                            double startSecs = Convert.ToDouble(conv.Attributes["startTime"].Value.Substring(2, conv.Attributes["startTime"].Value.Length - 3)) - recStartSecs;
                                            double endSecs = Convert.ToDouble(conv.Attributes["endTime"].Value.Substring(2, conv.Attributes["endTime"].Value.Length - 3)) - recStartSecs;
                                            DateTime start = Utilities.geFullTime(recStartTime.AddSeconds(startSecs));
                                            DateTime end = Utilities.geFullTime(recStartTime.AddSeconds(endSecs));
                                            double dbAvg = Convert.ToDouble(conv.Attributes["average_dB"].Value);
                                            double dbPeak = Convert.ToDouble(conv.Attributes["peak_dB"].Value);
                                            double bd = (end - start).Seconds + ((end - start).Milliseconds > 0 ? ((end - start).Milliseconds / 1000.00) : 0.00); //endSecs - startSecs;
                                            if (Utilities.isSameDay(start, classDay) &&
                                            start.Hour >= startHour &&
                                            (start.Hour < endHour || (start.Hour == endHour && start.Minute <= endMinute)) &&
                                            start>=pdi.startDate &&
                                            start<=pdi.endDate)
                                            { 
                                                if (conv.Name == "Conversation")
                                                { 
                                                    double tc = Convert.ToDouble(conv.Attributes["turnTaking"].Value);

                                                    if (tc > 0)
                                                    {
                                                        // DEBUG DELETE Tuple<String, String, String> logActs = getLogActivities(start, end, false, "");
                                                        if (start.Month==1 && start.Year==2019 && start.Day==23 && start.Hour==9 && (start.Minute==0 || start.Minute==1))
                                                        {
                                                            Boolean stop = true;
                                                        }

                                                        Tuple<String, String, String, String, String> logActs = getLogActivities(start, end, true, Utilities.getNumberIdFromPerson(pi));
                                                        sw.WriteLine(Path.GetFileName(itsFile) + "," +
                                                                                        classDay + "," +
                                                                                        pi.shortId + "," +
                                                                                        pdi.lenaId + "," +
                                                                                        pi.subjectType + "," +
                                                                                        segmentNumber +
                                                                                        ",Conversation_turnTaking," +
                                                                                        Utilities.getTimeStr(recStartTime) + "," +
                                                                                        startSecs + "," +
                                                                                        endSecs + "," +
                                                                                        Utilities.getTimeStr(start) + "," +
                                                                                        Utilities.getTimeStr(end) + "," +
                                                                                        String.Format("{0:0.00}", 0) + "," +
                                                                                        String.Format("{0:0.00}", bd) + "," +
                                                                                        "," +
                                                                                        String.Format("{0:0.00}", dbAvg) + "," +
                                                                                        String.Format("{0:0.00}", dbPeak) + "," +
                                                                                        String.Format("{0:0.00}", tc) + "," +
                                                                                        logActs.Item1  +","+
                                                                                        logActs.Item4 + "," +
                                                                                        logActs.Item5);
//sw.WriteLine("File,Date,Subject,LenaID,SubjectType,segmentid,voctype,recstart,startsec,endsec,starttime,endtime,duration,seg_duration,count,avg_db,avg_peak,turn_taking ");
                                            LenaOnset lenaOnset = new LenaOnset();
                                            lenaOnset.itsFile = itsFile;
                                            lenaOnset.lenaId = pdi.lenaId;
                                            lenaOnset.segmentId = segmentNumber.ToString() ;
                                            lenaOnset.recStartTime = recStartTime;
                                            lenaOnset.startSec = startSecs;
                                            lenaOnset.endSec = endSecs;
                                            lenaOnset.segmentDurSecs = 0;
                                                        lenaOnset.id = pi.mapId;
                                                        lenaOnset.type = "Conversation_turnTaking";
                                                        lenaOnset.durSecs = bd;
                                                        lenaOnset.tc = tc;
                                                        lenaOnset.startTime = start;
                                                        lenaOnset.parentStartTime = start;
                                                        lenaOnset.endTime = end;
                                                        lenaOnset.avgDb = dbAvg;
                                                        lenaOnset.peakDb = dbPeak;
                                            lenaOnset.subjectType = pi.subjectType;
                                                        if (!lenaOnsets.ContainsKey(lenaOnset.id))
                                                            lenaOnsets.Add(lenaOnset.id, new List<LenaOnset>());
                                                        lenaOnsets[lenaOnset.id].Add(lenaOnset);

                                                    }

                                                }

                                             
                                            foreach (XmlNode seg in segments)
                                            {
                                                segmentNumber++;
                                                startSecs = Convert.ToDouble(seg.Attributes["startTime"].Value.Substring(2, seg.Attributes["startTime"].Value.Length - 3)) - recStartSecs;
                                                endSecs = Convert.ToDouble(seg.Attributes["endTime"].Value.Substring(2, seg.Attributes["endTime"].Value.Length - 3)) - recStartSecs;
                                                start = Utilities.geFullTime(recStartTime.AddMilliseconds(startSecs * 1000));
                                                end = Utilities.geFullTime(recStartTime.AddMilliseconds(endSecs * 1000));
                                                bd = (end - start).Seconds + ((end - start).Milliseconds > 0 ? ((end - start).Milliseconds / 1000.00) : 0); //endSecs - startSecs;
                                                dbAvg = Convert.ToDouble(seg.Attributes["average_dB"].Value);
                                                dbPeak = Convert.ToDouble(seg.Attributes["peak_dB"].Value);
                                                String speaker = seg.Attributes["spkr"].Value;

                                                LenaOnset lenaSegmentOnset = new LenaOnset();
                                        lenaSegmentOnset.itsFile = itsFile;
                                        lenaSegmentOnset.lenaId = pdi.lenaId;
                                        lenaSegmentOnset.segmentId = segmentNumber.ToString();
                                        lenaSegmentOnset.recStartTime = recStartTime;
                                        lenaSegmentOnset.startSec = startSecs;
                                        lenaSegmentOnset.endSec = endSecs;
                                        lenaSegmentOnset.id = pi.mapId;
                                                lenaSegmentOnset.startTime = start;
                                                lenaSegmentOnset.parentStartTime = lenaSegmentOnset.startTime;
                                                lenaSegmentOnset.endTime = end;
                                                lenaSegmentOnset.segmentDurSecs = bd;
                                                lenaSegmentOnset.avgDb = dbAvg;
                                                lenaSegmentOnset.peakDb = dbPeak;
                                                lenaSegmentOnset.subjectType = pi.subjectType;
                                                Tuple<String, String, String, String, String> logActs = getLogActivities(start, end, true, Utilities.getNumberIdFromPerson(pi));
                                                    if (logActs.Item1 != "" && logActs.Item4 == "")
                                                    {
                                                        Boolean t = true;
                                                    }


                                                switch (speaker)
                                                {
                                                    case "CHN":
                                                    case "CHF":


                                                            double pivd = Convert.ToDouble(seg.Attributes["childUttLen"].Value.Substring(1, seg.Attributes["childUttLen"].Value.Length - 2));
                                                        double pivc = Convert.ToDouble(seg.Attributes["childUttCnt"].Value);
                                                        //sw.WriteLine("File,Date,Subject,LenaID,SubjectType,segmentid,voctype,recstart,startsec,endsec,starttime,endtime,duration,
                                                        //seg_duration,wordcount,avg_db,avg_peak,turn_taking ");
                                                        sw.WriteLine(Path.GetFileName(itsFile) + "," +
                                                                                classDay + "," +
                                                                                pi.shortId + "," +
                                                                                pdi.lenaId + "," +
                                                                                pi.subjectType + "," +
                                                                                segmentNumber +
                                                                                ",CHN_CHF SegmentUttCount," +
                                                                                Utilities.getTimeStr(recStartTime) + "," +
                                                                                startSecs + "," +
                                                                                endSecs + "," +
                                                                                Utilities.getTimeStr(start) + "," +
                                                                                Utilities.getTimeStr(end) + "," +
                                                                                String.Format("{0:0.00}", pivd) + "," +
                                                                                String.Format("{0:0.00}", bd) + "," +
                                                                                "," +
                                                                                String.Format("{0:0.00}", dbAvg) + "," +
                                                                                String.Format("{0:0.00}", dbPeak) + "," + "," +
                                                                                        logActs.Item1 +","+
                                                                                        logActs.Item4 + "," +
                                                                                        logActs.Item5); 

                                                        lenaSegmentOnset.type = "CHN_CHF SegmentUttCount";
                                                        lenaSegmentOnset.durSecs = pivd;
                                                        lenaSegmentOnset.parentStartTime = lenaSegmentOnset.startTime;
                                                        lenaSegmentOnset.count = pivc;
                                                        if (!lenaOnsets.ContainsKey(lenaSegmentOnset.id))
                                                            lenaOnsets.Add(lenaSegmentOnset.id, new List<LenaOnset>());
                                                        lenaOnsets[lenaSegmentOnset.id].Add(lenaSegmentOnset);

                                                personDayMappings[pi.mapId].totalLenaVars.avgDb += dbAvg;
                                                personDayMappings[pi.mapId].totalLenaVars.maxDb += dbPeak;
                                                personDayMappings[pi.mapId].totalLenaVars.totalSegments += 1;


                                                foreach (XmlAttribute atts in seg.Attributes)
                                                        {
                                                            if (atts.Name.IndexOf("startCry") == 0)
                                                            {
                                                                String attStep = atts.Name.Substring(8);
                                                                String att = atts.Name;
                                                                double astartSecs = Convert.ToDouble(seg.Attributes[att].Value.Substring(2, seg.Attributes[att].Value.Length - 3)) - recStartSecs;
                                                                double aendSecs = Convert.ToDouble(seg.Attributes["endCry" + attStep].Value.Substring(2, seg.Attributes["endCry" + attStep].Value.Length - 3)) - recStartSecs;
                                                                DateTime astart = Utilities.geFullTime(recStartTime.AddMilliseconds(astartSecs * 1000));
                                                                DateTime aend = Utilities.geFullTime(recStartTime.AddMilliseconds(aendSecs * 1000));
                                                                double apicry = (aend - astart).Seconds + ((aend - astart).Milliseconds > 0 ? (aend - astart).Milliseconds / 1000.00 : 0); //cendSecs - cstartSecs;
                                                                Tuple<String, String, String, String, String> logCryActs = getLogActivities(astart, aend, true, Utilities.getNumberIdFromPerson(pi));

                                                                    sw.WriteLine(Path.GetFileName(itsFile) + "," +
                                                                                classDay + "," +
                                                                                pi.shortId + "," +
                                                                                pdi.lenaId + "," +
                                                                                pi.subjectType + "," +
                                                                                segmentNumber +
                                                                                ",CHN_CHF CryDur," +
                                                                                Utilities.getTimeStr(recStartTime) + "," +
                                                                                astartSecs + "," +
                                                                                aendSecs + "," +
                                                                                Utilities.getTimeStr(astart) + "," +
                                                                                Utilities.getTimeStr(aend) + "," +
                                                                                String.Format("{0:0.00}", apicry) + "," +
                                                                                String.Format("{0:0.00}", bd) + "," +
                                                                                "," +
                                                                                "," +
                                                                                "," + "," +
                                                                                        logCryActs.Item1  +","+
                                                                                        logCryActs.Item4 + "," +
                                                                                        logCryActs.Item5); //String.Format("{0:0.00}", dbPeak) + ",");




                                                                    LenaOnset lenaAttOnset = new LenaOnset();
                                                        lenaAttOnset.itsFile = itsFile;
                                                        lenaAttOnset.lenaId = pdi.lenaId;
                                                        lenaAttOnset.segmentId = segmentNumber.ToString();
                                                        lenaAttOnset.recStartTime = recStartTime;
                                                        lenaAttOnset.startSec = astartSecs;
                                                        lenaAttOnset.endSec = aendSecs;
                                                        lenaAttOnset.id = pi.mapId;
                                                                lenaAttOnset.startTime = astart;
                                                                lenaAttOnset.endTime = aend;
                                                                lenaAttOnset.segmentDurSecs = bd;
                                                                lenaAttOnset.avgDb = dbAvg;
                                                                lenaAttOnset.peakDb = dbPeak;
                                                                lenaAttOnset.type = "CHN_CHF CryDur";
                                                                lenaAttOnset.durSecs = apicry;
                                                                lenaAttOnset.count = 0;
                                                        lenaAttOnset.parentStartTime = lenaSegmentOnset.startTime;

                                                        lenaAttOnset.subjectType = pi.subjectType;
                                                        if (!lenaOnsets.ContainsKey(lenaAttOnset.id))
                                                                    lenaOnsets.Add(lenaAttOnset.id, new List<LenaOnset>());
                                                                lenaOnsets[lenaAttOnset.id].Add(lenaAttOnset);

                                                            }
                                                            else if (atts.Name.IndexOf("startUtt") == 0)
                                                            {
                                                                String attStep = atts.Name.Substring(8);
                                                                String att = atts.Name;
                                                                double astartSecs = Convert.ToDouble(seg.Attributes[att].Value.Substring(2, seg.Attributes[att].Value.Length - 3)) - recStartSecs;
                                                                double aendSecs = Convert.ToDouble(seg.Attributes["endUtt" + attStep].Value.Substring(2, seg.Attributes["endUtt" + attStep].Value.Length - 3)) - recStartSecs;
                                                                DateTime astart = Utilities.geFullTime(recStartTime.AddMilliseconds(astartSecs * 1000));
                                                                DateTime aend = Utilities.geFullTime(recStartTime.AddMilliseconds(aendSecs * 1000));
                                                                double apiutts = (aend - astart).Seconds + ((aend - astart).Milliseconds > 0 ? (aend - astart).Milliseconds / 1000.00 : 0); //cendSecs - cstartSecs;
                                                                Tuple<String, String, String, String, String> logUttActs = getLogActivities(astart, aend, true, Utilities.getNumberIdFromPerson(pi));
                                                                    sw.WriteLine(Path.GetFileName(itsFile) + "," +
                                                                                classDay + "," +
                                                                                pi.shortId + "," +
                                                                                pdi.lenaId + "," +
                                                                                pi.subjectType + "," +
                                                                                segmentNumber +
                                                                                ",CHN_CHF UttDur," +
                                                                                Utilities.getTimeStr(recStartTime) + "," +
                                                                                astartSecs + "," +
                                                                                aendSecs + "," +
                                                                                Utilities.getTimeStr(astart) + "," +
                                                                                Utilities.getTimeStr(aend) + "," +
                                                                                String.Format("{0:0.00}", apiutts) + "," +
                                                                                String.Format("{0:0.00}", bd) + "," +
                                                                                "," +
                                                                                "," +
                                                                                "," + "," +
                                                                                        logUttActs.Item1  +","+
                                                                                        logUttActs.Item4 + "," +
                                                                                        logUttActs.Item5);   //String.Format("{0:0.00}", dbPeak) + ",");

                                                                    LenaOnset lenaAttOnset = new LenaOnset();
                                                        lenaAttOnset.itsFile = itsFile;
                                                        lenaAttOnset.lenaId = pdi.lenaId;
                                                        lenaAttOnset.segmentId = segmentNumber.ToString();
                                                        lenaAttOnset.recStartTime = recStartTime;
                                                        lenaAttOnset.startSec = astartSecs;
                                                        lenaAttOnset.endSec = aendSecs;

                                                        lenaAttOnset.id = pi.mapId;
                                                                lenaAttOnset.startTime = astart;
                                                                lenaAttOnset.endTime = aend;
                                                                lenaAttOnset.segmentDurSecs = bd;
                                                                lenaAttOnset.avgDb = dbAvg;
                                                                lenaAttOnset.peakDb = dbPeak;
                                                                lenaAttOnset.type = "CHN_CHF UttDur";
                                                                lenaAttOnset.durSecs = apiutts;
                                                                lenaAttOnset.count = 0;
                                                        lenaAttOnset.parentStartTime = lenaSegmentOnset.startTime;

                                                        lenaAttOnset.subjectType = pi.subjectType;
                                                        if (!lenaOnsets.ContainsKey(lenaAttOnset.id))
                                                                    lenaOnsets.Add(lenaAttOnset.id, new List<LenaOnset>());
                                                                lenaOnsets[lenaAttOnset.id].Add(lenaAttOnset);
                                                            }
                                                        }
                                                        break;
                                                    case "FAN":
                                                    case "MAN":
                                                        Boolean isFemale = speaker == "FAN";
                                                        double piac = isFemale ? Convert.ToDouble(seg.Attributes["femaleAdultWordCnt"].Value) : Convert.ToDouble(seg.Attributes["maleAdultWordCnt"].Value);
                                                        double piad = isFemale ? Convert.ToDouble(seg.Attributes["femaleAdultUttLen"].Value.Substring(1, seg.Attributes["femaleAdultUttLen"].Value.Length - 2)) : Convert.ToDouble(seg.Attributes["maleAdultUttLen"].Value.Substring(1, seg.Attributes["maleAdultUttLen"].Value.Length - 2));
                                                        piad = bd;

                                                        sw.WriteLine(Path.GetFileName(itsFile) + "," +
                                                                            classDay + "," +
                                                                            pi.shortId + "," +
                                                                            pdi.lenaId + "," +
                                                                            pi.subjectType + "," +
                                                                            segmentNumber +
                                                                            (isFemale ? ",FAN SegmentUtt," : ",MAN SegmentUtt,") +
                                                                            Utilities.getTimeStr(recStartTime) + "," +
                                                                            startSecs + "," +
                                                                            endSecs + "," +
                                                                            Utilities.getTimeStr(start) + "," +
                                                                            Utilities.getTimeStr(end) + "," +
                                                                            String.Format("{0:0.00}", piad) + "," +
                                                                            String.Format("{0:0.00}", bd) + "," +
                                                                            String.Format("{0:0.00}", piac) + "," +
                                                                            String.Format("{0:0.00}", dbAvg) + "," +
                                                                            String.Format("{0:0.00}", dbPeak) + "," + "," +
                                                                                        logActs.Item1 +","+
                                                                                        logActs.Item4 + "," +
                                                                                        logActs.Item5); 

                                                            lenaSegmentOnset.type = (isFemale ? "FAN SegmentUtt" : "MAN SegmentUtt");
                                                        lenaSegmentOnset.durSecs = piad;
                                                        lenaSegmentOnset.count = piac;
                                                        if (!lenaOnsets.ContainsKey(lenaSegmentOnset.id))
                                                            lenaOnsets.Add(lenaSegmentOnset.id, new List<LenaOnset>());
                                                        lenaOnsets[lenaSegmentOnset.id].Add(lenaSegmentOnset);


                                                        break;


                                                    case "CXN":
                                                    case "CXF":
                                                sw.WriteLine(Path.GetFileName(itsFile) + "," +
                                                                     classDay + "," +
                                                                     pi.shortId + "," +
                                                                     pdi.lenaId + "," +
                                                                     pi.subjectType + "," +
                                                                     segmentNumber +
                                                                     ",CXN_CXF SegmentUttDur," +
                                                                     Utilities.getTimeStr(recStartTime) + "," +
                                                                     startSecs + "," +
                                                                     endSecs + "," +
                                                                     Utilities.getTimeStr(start) + "," +
                                                                     Utilities.getTimeStr(end) + "," +
                                                                     String.Format("{0:0.00}", 0) + "," +
                                                                     String.Format("{0:0.00}", bd) + "," +
                                                                     "0.00," +
                                                                     String.Format("{0:0.00}", dbAvg) + "," +
                                                                     String.Format("{0:0.00}", dbPeak) + "," + "," +
                                                                                logActs.Item1 + "," +
                                                                                logActs.Item4 + "," +
                                                                                logActs.Item5);

                                                lenaSegmentOnset.type = "CXN_XF SegmentUttDur";
                                                lenaSegmentOnset.durSecs = lenaSegmentOnset.segmentDurSecs;
                                                lenaSegmentOnset.count = 0;
                                                if (!lenaOnsets.ContainsKey(lenaSegmentOnset.id))
                                                    lenaOnsets.Add(lenaSegmentOnset.id, new List<LenaOnset>());
                                                lenaOnsets[lenaSegmentOnset.id].Add(lenaSegmentOnset);

                                                break;
                                            case "OLN":
                                                sw.WriteLine(Path.GetFileName(itsFile) + "," +
                                                                    classDay + "," +
                                                                    pi.shortId + "," +
                                                                    pdi.lenaId + "," +
                                                                    pi.subjectType + "," +
                                                                    segmentNumber +
                                                                    ",OLNDur," +
                                                                    Utilities.getTimeStr(recStartTime) + "," +
                                                                    startSecs + "," +
                                                                    endSecs + "," +
                                                                    Utilities.getTimeStr(start) + "," +
                                                                    Utilities.getTimeStr(end) + "," +
                                                                    String.Format("{0:0.00}", 0) + "," +
                                                                    String.Format("{0:0.00}", bd) + "," +
                                                                    "0.00," +
                                                                    String.Format("{0:0.00}", dbAvg) + "," +
                                                                    String.Format("{0:0.00}", dbPeak) + "," + "," +
                                                                               logActs.Item1 + "," +
                                                                               logActs.Item4 + "," +
                                                                               logActs.Item5);

                                                lenaSegmentOnset.type = "OLNDur";
                                                lenaSegmentOnset.durSecs = lenaSegmentOnset.segmentDurSecs;
                                                lenaSegmentOnset.count = 0;
                                                if (!lenaOnsets.ContainsKey(lenaSegmentOnset.id))
                                                    lenaOnsets.Add(lenaSegmentOnset.id, new List<LenaOnset>());
                                                lenaOnsets[lenaSegmentOnset.id].Add(lenaSegmentOnset);

                                                break;
                                            case "NON":
                                                sw.WriteLine(Path.GetFileName(itsFile) + "," +
                                                                   classDay + "," +
                                                                   pi.shortId + "," +
                                                                   pdi.lenaId + "," +
                                                                   pi.subjectType + "," +
                                                                   segmentNumber +
                                                                   ",NONDur," +
                                                                   Utilities.getTimeStr(recStartTime) + "," +
                                                                   startSecs + "," +
                                                                   endSecs + "," +
                                                                   Utilities.getTimeStr(start) + "," +
                                                                   Utilities.getTimeStr(end) + "," +
                                                                   String.Format("{0:0.00}", 0) + "," +
                                                                   String.Format("{0:0.00}", bd) + "," +
                                                                   "0.00," +
                                                                   String.Format("{0:0.00}", dbAvg) + "," +
                                                                   String.Format("{0:0.00}", dbPeak) + "," + "," +
                                                                              logActs.Item1 + "," +
                                                                              logActs.Item4 + "," +
                                                                              logActs.Item5);

                                                lenaSegmentOnset.type = "NONDur";
                                                lenaSegmentOnset.durSecs = lenaSegmentOnset.segmentDurSecs;
                                                lenaSegmentOnset.count = 0;
                                                if (!lenaOnsets.ContainsKey(lenaSegmentOnset.id))
                                                    lenaOnsets.Add(lenaSegmentOnset.id, new List<LenaOnset>());
                                                lenaOnsets[lenaSegmentOnset.id].Add(lenaSegmentOnset);

                                                break;

                                            default:
                                                             sw.WriteLine(Path.GetFileName(itsFile) + "," +
                                                                                classDay + "," +
                                                                                pi.shortId + "," +
                                                                                pdi.lenaId + "," +
                                                                                pi.subjectType + "," +
                                                                                segmentNumber +","+
                                                                                speaker+","+
                                                                                Utilities.getTimeStr(recStartTime) + "," +
                                                                                startSecs + "," +
                                                                                endSecs + "," +
                                                                                Utilities.getTimeStr(start) + "," +
                                                                                Utilities.getTimeStr(end) + "," +
                                                                                String.Format("{0:0.00}", bd) + "," +
                                                                                String.Format("{0:0.00}", bd) + "," +
                                                                                String.Format("{0:0.00}", 0) + "," +
                                                                                String.Format("{0:0.00}", dbAvg) + "," +
                                                                                String.Format("{0:0.00}", dbPeak) + "," + "," +
                                                                                            logActs.Item1 + "," +
                                                                                            logActs.Item4 + "," +
                                                                                            logActs.Item5);


                                                lenaSegmentOnset.type = speaker;
                                                lenaSegmentOnset.durSecs = lenaSegmentOnset.segmentDurSecs;
                                                lenaSegmentOnset.count = 0;
                                                if (!lenaOnsets.ContainsKey(lenaSegmentOnset.id))
                                                    lenaOnsets.Add(lenaSegmentOnset.id, new List<LenaOnset>());
                                                lenaOnsets[lenaSegmentOnset.id].Add(lenaSegmentOnset);
                                                break;


                                                    }
                                                }
                                        }
                                            else
                                            {
                                                //DEBUG DELETE
                                                Boolean stop = true;
                                            }

                                        }
                                }

                            }

                        }
                        sw.Close();

                    }

                    public PersonDayInfo getPersonInfoByLena(String lenaId)
                    {
                        foreach(PersonDayInfo pdi in personDayMappings.Values)
                        {
                            if(pdi.lenaId==lenaId && pdi.present && pdi.status == "PRESENT")
                            {
                                return pdi;
                            }
                        }
                        return new PersonDayInfo();
                    }
                    public double linearInterpolate(DateTime t, DateTime t1, double y0, DateTime t2, double y1)
                    {
                        double x0 = t1.Minute * 60000 + t1.Second * 1000 + t1.Millisecond;
                        double x1 = t2.Minute * 60000 + t2.Second * 1000 + t2.Millisecond;
                        double x = t.Minute * 60000 + t.Second * 1000 + t.Millisecond;
                        double lerp = (y0 * (x1 - x) + y1 * (x - x0)) / (x1 - x0);
                        return lerp;
                    }
                    public Tuple<double, double> linearInterpolate(DateTime t, DateTime t1, double xa,  double ya , DateTime t2, double xb, double yb)
                    {
                        double x0 = t1.Minute * 60000 + t1.Second * 1000 + t1.Millisecond;
                        double x1 = t2.Minute * 60000 + t2.Second * 1000 + t2.Millisecond;
                        double x = t.Minute * 60000 + t.Second * 1000 + t.Millisecond;
                        /**** got ms totLA***/

                    double y0x = xa;
            double y1x = xb;
            double y0y = ya;
            double y1y = yb;

            double xlerp = (y0x * (x1 - x) + y1x * (x - x0)) / (x1 - x0);
            double ylerp = (y0y * (x1 - x) + y1y * (x - x0)) / (x1 - x0);
            return new Tuple<double, double>(xlerp, ylerp);
        }
        public void setTenthOfSecUbi()
        {
            Dictionary<DateTime, Dictionary<String, PersonInfo>> ubiTenthsL = getTenthOfSecUbi(ubiLocationsL);
            Dictionary<DateTime, Dictionary<String, PersonInfo>> ubiTenthsR = getTenthOfSecUbi(ubiLocationsR);
             
            ubiLocationsL.Clear();
            ubiLocationsR.Clear();

            foreach (DateTime szTimeStamp in ubiTenthsL.Keys)
            {
                if (ubiTenthsR.ContainsKey(szTimeStamp))
                {
                    foreach (String person in ubiTenthsL[szTimeStamp].Keys)
                    {
                        if(person.Contains("14"))
                        {
                            Boolean stop = true;
                        }
                        if (ubiTenthsR[szTimeStamp].ContainsKey(person))
                        {
                            PersonSuperInfo psi = new PersonSuperInfo();
                            psi.xl = ubiTenthsL[szTimeStamp][person].x;
                            psi.yl = ubiTenthsL[szTimeStamp][person].y;
                            psi.xr = ubiTenthsR[szTimeStamp][person].x;
                            psi.yr = ubiTenthsR[szTimeStamp][person].y;

                            Tuple<double, double, double> xyo = Utilities.getCenterAndOrientationFromLR(psi);
                            psi.x = xyo.Item1;
                            psi.y = xyo.Item2;
                            psi.ori_chaoming = xyo.Item3;

                            if (!ubiTenths.ContainsKey(szTimeStamp))
                            {
                                ubiTenths.Add(szTimeStamp, new Dictionary<string, PersonSuperInfo>());
                            }
                            if (!ubiTenths[szTimeStamp].ContainsKey(person))
                            {
                                ubiTenths[szTimeStamp].Add(person, psi);
                            }
                             
                        }
                    }
                }
                 

            }
        }
        public void setTenthOfSecLENA()
        {
            foreach(String person in lenaOnsets.Keys)
            {
                foreach(LenaOnset lenaOnset in lenaOnsets[person])
                {
                    DateTime time = lenaOnset.startTime;
                    int ms = time.Millisecond > 0 ? time.Millisecond / 100 * 100 : time.Millisecond;// + 100;
                    time = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, ms);
                    DateTime timeEnd = lenaOnset.startTime.AddSeconds(lenaOnset.durSecs);
                    ms = timeEnd.Millisecond > 0 ? timeEnd.Millisecond / 100 * 100 : timeEnd.Millisecond;// + 100;
                    timeEnd = new DateTime(timeEnd.Year, timeEnd.Month, timeEnd.Day, timeEnd.Hour, timeEnd.Minute, timeEnd.Second, ms);

                    double blockDur = (timeEnd - time).Seconds + (timeEnd - time).Milliseconds > 0 ? ((timeEnd - time).Milliseconds / 1000.00) : 0;
                    double vocDur10 = (lenaOnset.durSecs / blockDur) / 10;
                    double vocCount10 = (lenaOnset.count / blockDur) / 10;
                    //double avgDb10 = (lenaOnset.avgDb / blockDur) / 10;
                    //double avgDb10 = (lenaOnset.peakDb / blockDur) / 10;
                    Boolean setDbs = true;

                    Boolean personExists = personDayMappings.ContainsKey(person);
                    if( personExists && blockDur>0)
                    {
                        if (lenaOnset.type != "Conversation_turnTaking" && lenaOnset.type != "CHN_CHF CryDur" && lenaOnset.type != "CHN_CHF UttDur")
                        {
                            personDayMappings[person].totalLenaVars.avgDb += lenaOnset.avgDb;
                            personDayMappings[person].totalLenaVars.maxDb += lenaOnset.peakDb;
                            personDayMappings[person].totalLenaVars.totalSegments += 1;
                        }

                        do
                        {
                           Boolean WUBI = ubiTenths.ContainsKey(time) && ubiTenths[time].ContainsKey(person);
                            if (WUBI && lenaOnset.type != "Conversation_turnTaking" && lenaOnset.type != "CHN_CHF CryDur" && lenaOnset.type != "CHN_CHF UttDur")
                            {
                                personDayMappings[person].WUBILenaVars.avgDb += lenaOnset.avgDb;
                                personDayMappings[person].WUBILenaVars.maxDb += lenaOnset.peakDb;
                                personDayMappings[person].WUBILenaVars.totalSegments += 1;
                                setDbs = false;
                            }


                            switch (lenaOnset.type)
                            {
                                case "Conversation_turnTaking":
                                    if (personExists)//DEBUG FIX TAKE OFF
                                    {
                                        personDayMappings[person].totalLenaVars.totalTurnCounts += vocCount10;
                                        if (WUBI)
                                        {
                                            ubiTenths[time][person].lenaVars.totalTurnCounts += vocCount10;
                                            personDayMappings[person].WUBILenaVars.totalTurnCounts += vocCount10;
                                        }
                                    }
                                    break;
                                
                                case "CHN_CHF CryDur":

                                    if (personExists)
                                    {
                                        personDayMappings[person].totalLenaVars.totalChildCryDuration += .1;
                                        if (WUBI)
                                        {
                                            ubiTenths[time][person].lenaVars.totalChildCryDuration += vocDur10;
                                            personDayMappings[person].WUBILenaVars.totalChildCryDuration += .1;
                                        }
                                    }

                                    break;
                                case "CHN_CHF UttDur":

                                    if(personExists)
                                    {
                                        personDayMappings[person].totalLenaVars.totalChildUttDuration += .1;
                                        if (WUBI)
                                        {//CHECK WHY THE OR DEBUG
                                            ubiTenths[time][person].wasTalking = lenaOnset.durSecs > 0 || ubiTenths[time][person].wasTalking;//, vocCount10, turnCount10, vocDur10, adults10, noise10;
                                            ubiTenths[time][person].lenaVars.totalChildUttDuration += vocDur10;
                                            personDayMappings[person].WUBILenaVars.totalChildUttDuration += vocDur10;
                                        }
                                    }
                                    
                                    break;
                                case "CHN_CHF SegmentUttCount":

                                    if (personExists)
                                    {
                                        personDayMappings[person].totalLenaVars.totalChildUttCount += vocCount10;
                                        if (WUBI)
                                        {
                                            ubiTenths[time][person].lenaVars.totalChildUttCount += vocCount10;
                                            personDayMappings[person].WUBILenaVars.totalChildUttCount += vocCount10;
                                        }
                                    } 
                                    break;
                                case "FAN SegmentUtt":

                                    if (personExists)
                                    {
                                        personDayMappings[person].totalLenaVars.totalAdultWordCount += vocCount10;
                                        if (WUBI)
                                        {
                                            ubiTenths[time][person].lenaVars.totalAdultWordCount += vocCount10;
                                            personDayMappings[person].WUBILenaVars.totalAdultWordCount += vocCount10;
                                        }
                                    }
                                     
                                    break;
                                case "MAN SegmentUtt":

                                    if (personExists)
                                    {
                                        personDayMappings[person].totalLenaVars.totalAdultWordCount += vocCount10;
                                        if (WUBI)
                                        {
                                            ubiTenths[time][person].lenaVars.totalAdultWordCount += vocCount10;
                                            personDayMappings[person].WUBILenaVars.totalAdultWordCount += vocCount10;
                                        }
                                    }

                                     
                                    break;

                                case "OLN Dur":

                                    if (personExists)
                                    {
                                        personDayMappings[person].totalLenaVars.totalOLN += vocDur10;
                                        if (WUBI)
                                        {
                                            ubiTenths[time][person].lenaVars.totalOLN += vocDur10;
                                            personDayMappings[person].WUBILenaVars.totalOLN += vocDur10;
                                        }
                                    }


                                    break;

                                case "NON Dur":

                                    if (personExists)
                                    {
                                        personDayMappings[person].totalLenaVars.totalNoise += vocDur10;
                                        if (WUBI)
                                        {
                                            ubiTenths[time][person].lenaVars.totalNoise += vocDur10;
                                            personDayMappings[person].WUBILenaVars.totalNoise += vocDur10;
                                        }
                                    }


                                    break;
                            }
                             
                            time = time.AddMilliseconds(100);
                            //vocDur -= 0.1;
                            blockDur -= 0.1;
                        } while (blockDur > 0);
                    }

                }

            }
        }
        public void writeTenthOfSec(String dir)
        {
            TextWriter sw = new StreamWriter(dir);
            sw.WriteLine("BID, DateTime, X, Y, Chaoming_Orientation, Talking, Aid, S, Type,rx,ry,lx,ly");

            foreach (DateTime t in ubiTenths.Keys)
            {
                recSecs += .1;
                foreach (String p in ubiTenths[t].Keys)
                {
                    PersonSuperInfo psi = ubiTenths[t][p];
                    Person pi = personBaseMappings[p];
                    if(psi.wasTalking)
                    {
                        bool stop = true;
                    }
                    //sw.WriteLine("BID, DateTime, X, Y, Chaoming_Orientation, Talking, Aid, S, Type,rx,ry,lx,ly");
                    sw.WriteLine(pi.mapId + "," +
                        t.ToString("hh:mm:ss.fff tt") + "," + //t.ToLongTimeString() + "," +
                        psi.x + "," +
                        psi.y + "," +
                        psi.ori_chaoming + "," +
                        psi.wasTalking + "," +
                        pi.diagnosis + "," +
                        pi.gender + "," +
                        pi.subjectType + "," +
                        psi.xr + "," +
                        psi.yr + "," +
                        psi.xl + "," +
                        psi.yl  
                        );
                }

            }
            sw.Close();

        }

        public Dictionary<DateTime, Dictionary<String, PersonInfo>> getTenthOfSecUbi(Dictionary<String, List<UbiLocation>> ubiLocations)
        {
            //Dictionary<String, List<PersonInfo>> personTenthInfo = new Dictionary<string, List<PersonInfo>>();
            Dictionary<DateTime, Dictionary<String, PersonInfo>> dayActivities = new Dictionary<DateTime, Dictionary<string, PersonInfo>>();

            foreach (String personId in ubiLocations.Keys)
            {
                if (Utilities.getPerson(personDayMappings, personId).present && Utilities.getPerson(personDayMappings, personId).status=="PRESENT")
                {
                    ubiLocations[personId].OrderBy(order => order.time);
                    List<UbiLocation> ubiLoc = ubiLocations[personId];
                    //personTenthInfo.Add(personId, new List<PersonInfo>());
                    DateTime first = ubiLoc[0].time;//first date from merged file ordered by time
                    DateTime last = ubiLoc.Last().time;//last date from merged file ordered by time

                    //targets will begin at closest 100 ms multiple of start
                    int ms = first.Millisecond / 100 * 100 + 100;
                    if (first.Millisecond % 100 == 0)
                    {
                        ms -= 100;
                    }
                    DateTime target = new DateTime();//will be next .1 sec
                    if (ms == 1000)
                    {
                        if (first.Second == 59)
                        {
                            target = new DateTime(first.Year, first.Month, first.Day, first.Hour, first.Minute + 1, 0, 0);
                        }
                        else
                        {
                            target = new DateTime(first.Year, first.Month, first.Day, first.Hour, first.Minute, first.Second + 1, 0);
                        }
                    }
                    else
                    {
                        target = new DateTime(first.Year, first.Month, first.Day, first.Hour, first.Minute, first.Second, ms);
                    }
                    while (target.CompareTo(last) <= 0)
                    {
                        /******/
                        //find next time row based on ms
                        //PersonInfo pi = new PersonInfo();
                        UbiLocation pi = new UbiLocation();
                        pi.time = target;
                        int index = ubiLoc.BinarySearch(pi, new DateTimeComparer());
                        if (index < 0)
                        {
                            index = ~index;
                        }

                        if (index > 0)
                        {
                            //LQ: why same hour??
                            //FIX
                            //if ((raw[index - 1].dt.Hour == raw[index].dt.Hour) && (Math.Abs(raw[index - 1].dt.Minute - raw[index].dt.Minute) < 2))
                            //if ((Math.Abs(raw[index - 1].dt.Minute - raw[index].dt.Minute) < 2))
                            TimeSpan difference = ubiLoc[index].time.Subtract(ubiLoc[index - 1].time); // could also write `now - otherTime`
                            if (difference.TotalSeconds < 60)
                            {
                                Tuple<double, double> targetpoint = linearInterpolate(target, ubiLoc[index - 1].time, ubiLoc[index - 1].x, ubiLoc[index - 1].y, ubiLoc[index].time, ubiLoc[index].x, ubiLoc[index].y);
                                double orientation1 = 0;// ubiLoc[index - 1].ori;
                                double orientation2 = 0;// ubiLoc[index].ori;
                                double targetorientation = linearInterpolate(target, ubiLoc[index - 1].time, orientation1, ubiLoc[index].time, orientation2);
                                PersonInfo pi2 = new PersonInfo();
                                if (difference.TotalSeconds < 60)
                                {
                                    pi2.x = targetpoint.Item1;
                                    pi2.y = targetpoint.Item2;
                                }
                                else
                                {
                                    pi2.x = -5;
                                    pi2.y = -5;
                                }
                                pi2.time = target;
                                //pi2.ori = targetorientation;
                                //personTenthInfo[personId].Add(pi2);+
                                if (!dayActivities.ContainsKey(target))
                                    dayActivities.Add(target, new Dictionary<string, PersonInfo>());

                                if (!dayActivities[target].ContainsKey(personId))
                                    dayActivities[target].Add(personId, pi2);
                                //newList.Add(new Tuple<DateTime, PersonInfo>(target, pi2));// talk, vc, tc)));
                            }

                        }
                        target = target.AddMilliseconds(100);



                    }
                    maxTimes.Add(target.AddMilliseconds(-100));  
                }
            }
            return dayActivities;
        }
        public void getTenthOfSecReports()
        {
            //social onsets, approach, pairactivity
        }
        public DateTime getUbiTrunkTime()
        {
            /* gets max of maxUbiTimes. From the previous 10 min from the max, get the first time*/
            DateTime end = new DateTime();
            maxTimes = maxTimes.OrderBy(x => x.TimeOfDay).ToList();
            if (maxTimes.Count > 0)
            {
                end = maxTimes[maxTimes.Count - 1];
                foreach (DateTime dt in maxTimes)
                {
                    TimeSpan span = end.Subtract(dt);
                    if (span.TotalMinutes <= 10)
                    {
                        return dt;
                    }
                }
            }
            return end;

        }
        public void writeActivityLog(String szOutputFile)
        {
            String szDayFolder = Utilities.getDateDashStr(classDay);
            TextWriter sw = new StreamWriter(szOutputFile);
            String header = "Class (School_Class_AM/PM),Date (DD/MM/YY),Children_Included,Teachers_Included,Start Time " +
                "(HH:MM),End Time (HH:MM),Number of Children,Number of Adults,Activity,AcLog_Comments,Questions";
            int hCount = header.Split(',').Length;
            header += ",CHILD," +
                "Conversation_turnTaking COUNT,Conversation_turnTaking DUR," +
                "CHN_CHF SegmentUtt COUNT,CHN_CHF SegmentUttDUR," +
                "CHN_CHF Utt COUNT,CHN_CHF Utt DUR," +
                "CHN_CHF Cry COUNT,CHN_CHF Cry DUR," +
                "FAN MAN SegmentUtt COUNT,FAN SegmentUtt DUR," +
                "CXN_CXF DUR, AVG_AVGDB, AVG_PEAKDB";

            sw.WriteLine(header);//,children,teachers");


            Dictionary<String, int> childStamps = new Dictionary<string, int>();
            foreach(Activity act in logActivities)
            {
                String[] children = act.szChildren.Split('|');
                foreach(String child in children)
                {
                    //if(lenaOnsets.ContainsKey())
                    double tcc = 0;
                    double svc = 0;
                    double suc = 0;
                    double scc = 0;
                    double sac = 0;
                    double solnc = 0;


                    double tcd = 0;
                    double svd = 0;
                    double sud = 0; 
                    double scd = 0;
                    double sad = 0;
                    double solnd = 0;
                    double cxd = 0;

                    double avgDb = 0;
                    double avgPeak = 0;
                    int iavgDb = 0;
                    int iavgPeak = 0;

                    foreach (String k in lenaOnsets.Keys)
                    {
                        if(lenaOnsets[k][0].subjectType.ToUpper().StartsWith("CHILD") && child== Utilities.getNumberIdFromChild(k))
                        {
                            int startFrom = childStamps.ContainsKey(child) ? childStamps[child] : 0;
                            while (startFrom< lenaOnsets[k].Count &&  lenaOnsets[k][startFrom].parentStartTime < act.start )
                            {
                                startFrom++;
                            }
                            while (startFrom < lenaOnsets[k].Count && lenaOnsets[k][startFrom].parentStartTime >= act.start && lenaOnsets[k][startFrom].parentStartTime < act.end)
                            {
                                LenaOnset lo = lenaOnsets[k][startFrom];
                                switch (lo.type)
                                {
                                    case "Conversation_turnTaking":
                                        tcc += lo.count;
                                        tcd += lo.durSecs;
                                        break;
                                    case "CHN_CHF SegmentUtt":
                                    case "CHN_CHF SegmentUttCount":
                                        svc += lo.count;
                                        svd += lo.durSecs;
                                        avgDb  +=lo.avgDb;
                                        iavgDb++;
                                        avgPeak += lo.peakDb;
                                        iavgPeak++;
                                        break;
                                    case "FAN SegmentUtt":
                                    case "MAN SegmentUtt":
                                    case "FAN SegmentUttCount":
                                    case "MAN SegmentUttCount":
                                        sac += lo.count;
                                        sad += lo.durSecs;
                                        break;
                                    case "CHN_CHF Utt":
                                    case "CHN_CHF UttDur":
                                        suc += lo.count;
                                        sud += lo.durSecs;
                                        break;
                                    case "CHN_CHF Cry":
                                    case "CHN_CHF CryDur":
                                        scc += lo.count;
                                        scd += lo.durSecs;
                                        break;
                                    case "CXN_XF SegmentUtt":
                                    case "CXN_XF SegmentUttCount":
                                        cxd += lo.durSecs;
                                        break;

                                }

                                /* Conversation_turnTaking COUNT,DUR,CHN_CHF SegmentUtt COUNT,DUR,FAN SegmentUtt COUNT,DUR,MAN SegmentUtt COUNT,DUR,CHN_CHF Utt COUNT,DUR,CHN_CHF Cry COUNT,DUR";
                                                                            lenaOnset.type = "Conversation_turnTaking";
                                                                            lenaSegmentOnset.type = "CHN_CHF SegmentUtt";
                                                                            lenaSegmentOnset.type = (isFemale ? "FAN SegmentUtt" : "MAN SegmentUtt");
                                                                            lenaAttOnset.type = "CHN_CHF Utt"     "CXN_XF SegmentUtt"    ;*/




                                startFrom++;
                            }
                            childStamps[child] = startFrom;

                            //int extraCommas = hCount-act.line.Split(',').Length;
                            //int hCount = header.Split(',').Length;

                            int lCount = act.line.Split(',').Length;
                            if(lCount>hCount)
                            {
                                while(lCount>hCount)
                                {
                                    act.line = act.line.Substring(0, act.line.LastIndexOf(","));
                                    lCount--;
                                }
                            } 
                            else if (lCount < hCount)
                            {
                                while (lCount < hCount)
                                {
                                    act.line = act.line+",";
                                    lCount++;
                                }
                            } 
                            sw.WriteLine(act.line + ","+
                                child +","+
                                tcc + "," + tcd + "," + 
                                svc + "," + svd + "," + 
                                suc + ","  + sud + "," + 
                                scc + "," + scd + "," + 
                                sac + "," + sad + "," + 
                                cxd+","+  
                                (iavgDb!=0 && avgDb!=0? avgDb/iavgDb:0) +","+
                                (iavgPeak != 0 && avgPeak != 0 ? avgPeak / iavgPeak:0));
                                 
                            /*    "Conversation_turnTaking COUNT,Conversation_turnTaking DUR,"+
                "CHN_CHF SegmentUtt COUNT,CHN_CHF SegmentUttDUR," +
                "CHN_CHF Utt COUNT,CHN_CHF Utt DUR," +
                "CHN_CHF Cry COUNT,CHN_CHF Cry DUR"+
                "FAN MAN SegmentUtt COUNT,FAN SegmentUtt DUR," +
                "CXN_CXF DUR");//,children,teachers");*/

                        }
                    }

                }




            }
            sw.Close();



             
        }
        public void writeActivityLogByMin1(String szOutputFile)
        {


            String szDayFolder = Utilities.getDateDashStr(classDay);
            TextWriter sw = new StreamWriter(szOutputFile);
            String header = "Class (School_Class_AM/PM),Date (DD/MM/YY),Children_Included,Teachers_Included,Start Time " +
                "(HH:MM),End Time (HH:MM),Number of Children,Number of Adults,Activity,AcLog_Comments,Questions";
            int hCount = header.Split(',').Length;
            header += ",CHILD," +
                "Conversation_turnTaking COUNT,Conversation_turnTaking DUR," +
                "CHN_CHF SegmentUtt COUNT,CHN_CHF SegmentUttDUR," +
                "CHN_CHF Utt COUNT,CHN_CHF Utt DUR," +
                "CHN_CHF Cry COUNT,CHN_CHF Cry DUR" +
                "FAN MAN SegmentUtt COUNT,FAN SegmentUtt DUR," +
                "CXN_CXF DUR";

            sw.WriteLine(header);//,children,teachers");


            Dictionary<String, Dictionary<DateTime, List<LenaOnset>>> loms = new Dictionary<string, Dictionary<DateTime, List<LenaOnset>>>();
            foreach(String person in lenaOnsets.Keys)
            {
                if (lenaOnsets[person][0].subjectType.ToUpper().StartsWith("CHILD"))
                {
                    String p = Utilities.getNumberIdFromChild(person);
                    loms.Add(p, new Dictionary<DateTime, List<LenaOnset>>());
                    foreach (LenaOnset lo in lenaOnsets[person])
                    {
                        double lcs = lo.count / (lo.endTime.AddMilliseconds(-lo.endTime.Millisecond) - lo.startTime.AddMilliseconds(-lo.startTime.Millisecond)).TotalSeconds;
                        double lds = lo.durSecs / (lo.endTime.AddMilliseconds(-lo.endTime.Millisecond) - lo.startTime.AddMilliseconds(-lo.startTime.Millisecond)).TotalSeconds;

                        DateTime lsm = lo.startTime.AddSeconds(-lo.startTime.Second).AddMilliseconds(-lo.startTime.Millisecond);
                        DateTime lem = lo.endTime.AddSeconds(-lo.endTime.Second).AddMilliseconds(-lo.endTime.Millisecond);
                        DateTime ett = lo.endTime.AddMilliseconds(-lo.endTime.Millisecond);
                        DateTime stt = lo.startTime.AddMilliseconds(-lo.startTime.Millisecond);//.AddSeconds(-lo.startTime.Second).AddMinutes(1);

                        LenaOnset nlo = new LenaOnset();
                        double nloSecs = 0;

                        while (lsm != lem)
                        {
                            if (!loms[p].ContainsKey(lsm))
                                loms[p].Add(lsm, new List<LenaOnset>());

                            nlo = new LenaOnset();
                            nloSecs = (lsm.AddMinutes(1) - stt).TotalSeconds;
                            nlo.count = lcs * nloSecs;
                            nlo.durSecs = lds * nloSecs;
                            nlo.subjectType = lo.subjectType;
                            nlo.type = lo.type;
                            loms[p][lsm].Add(lo);

                            lsm = lem.AddMinutes(1);
                            stt = lsm;//.AddMinutes(1);
                        }

                        if (!loms[p].ContainsKey(lsm))
                            loms[p].Add(lsm, new List<LenaOnset>());

                        nlo = new LenaOnset();
                        nloSecs = (ett - stt).TotalSeconds;
                        if (nloSecs != 0)
                        {
                            nlo.count = lcs * nloSecs;
                            nlo.durSecs = lds * nloSecs;
                            nlo.subjectType = lo.subjectType;
                            nlo.type = lo.type;
                            loms[p][lsm].Add(lo);
                        }

                    }
                }
            }




             



            //Dictionary<String, int> childStamps = new Dictionary<string, int>();
            foreach (Activity act in logActivities)
            {
                String[] children = act.szChildren.Split('|');

                foreach (String child in children)
                {
                    //if(lenaOnsets.ContainsKey())
                    double tcc = 0;
                    double svc = 0;
                    double suc = 0;
                    double scc = 0;
                    double sac = 0;
                    double solnc = 0;


                    double tcd = 0;
                    double svd = 0;
                    double sud = 0;
                    double scd = 0;
                    double sad = 0;
                    double solnd = 0;
                    double cxd = 0;

                    DateTime stt = act.start;
                    while(stt<act.end)
                    {
                        try
                        {
                            List<LenaOnset> los = loms[child][stt];
                            foreach(LenaOnset lo in los)
                            {
                                switch (lo.type)
                                {
                                    case "Conversation_turnTaking":
                                        tcc += lo.count;
                                        tcd += lo.durSecs;
                                        break;
                                    case "CHN_CHF SegmentUtt":
                                        svc += lo.count;
                                        svd += lo.durSecs;
                                        break;
                                    case "FAN SegmentUtt":
                                    case "MAN SegmentUtt":
                                        sac += lo.count;
                                        sad += lo.durSecs;
                                        break;
                                    case "CHN_CHF Utt":
                                        suc += lo.count;
                                        sud += lo.durSecs;
                                        break;
                                    case
                                    "CHN_CHF Cry":
                                        scc += lo.count;
                                        scd += lo.durSecs;
                                        break;
                                    case "CXN_XF SegmentUtt":
                                        cxd += lo.durSecs;
                                        break;

                                }

                            }

                        }
                        catch(Exception e)
                        {

                        }

                        stt.AddMinutes(1);
                    }
                    sw.WriteLine(act.line + "," +
                               child + "," +
                               tcc + "," + tcd + "," +
                               svc + "," + svd + "," +
                               suc + "," + sud + "," +
                               scc + "," + scd + "," +
                               sac + "," + sad + "," +
                               cxd);


 

                }




            }

            sw.Close();




        }
        public void writeActivityLogByMin(String szOutputFile)
        {


            String szDayFolder = Utilities.getDateDashStr(classDay);
            TextWriter sw = new StreamWriter(szOutputFile);
            String header = "Class (School_Class_AM/PM),Date (DD/MM/YY),Children_Included,Teachers_Included,Start Time " +
                "(HH:MM),End Time (HH:MM),Number of Children,Number of Adults,Activity,AcLog_Comments,Questions";
            int hCount = header.Split(',').Length;
            header += ",CHILD," +
                "Conversation_turnTaking COUNT,Conversation_turnTaking DUR," +
                "CHN_CHF SegmentUtt COUNT,CHN_CHF SegmentUttDUR," +
                "CHN_CHF Utt COUNT,CHN_CHF Utt DUR," +
                "CHN_CHF Cry COUNT,CHN_CHF Cry DUR" +
                "FAN MAN SegmentUtt COUNT,FAN SegmentUtt DUR," +
                "CXN_CXF DUR";


            /* child + "," +
                                   tcc + "," + tcd + "," +
                                   svc + "," + svd + "," +
                                   suc + "," + sud + "," +
                                   scc + "," + scd + "," +
                                   sac + "," + sad + "," +
                                   cxd);

            sw.WriteLine(header);//,children,teachers");

            sw.Close();
            foreach (String person in lenaOnsets.Keys)
            {
                if (lenaOnsets[person][0].subjectType.ToUpper().StartsWith("CHILD"))
                {
                    sw = new StreamWriter(szOutputFile,true);
                    writePersonActivityLogByMin(person, ref sw);
                    String p = Utilities.getNumberIdFromChild(person);
                    sw.Close();

                }
            }
             

          //  sw.Close();


    */

        }

        public void writePersonActivityLogByMin(String person , ref TextWriter sw)
        {
            Dictionary<DateTime, List<LenaOnset>> loms = new  Dictionary<DateTime, List<LenaOnset>>();
            String p = Utilities.getNumberIdFromChild(person);
            int c = 0;
            //foreach (String person in lenaOnsets.Keys)
            {
               // if (lenaOnsets[person][0].subjectType.ToUpper().StartsWith("CHILD"))
                {
                    foreach (LenaOnset lo in lenaOnsets[person])
                    {
                        double lcs = lo.count / (lo.endTime.AddMilliseconds(-lo.endTime.Millisecond) - lo.startTime.AddMilliseconds(-lo.startTime.Millisecond)).TotalSeconds;
                        double lds = lo.durSecs / (lo.endTime.AddMilliseconds(-lo.endTime.Millisecond) - lo.startTime.AddMilliseconds(-lo.startTime.Millisecond)).TotalSeconds;

                        DateTime lsm = lo.startTime.AddSeconds(-lo.startTime.Second).AddMilliseconds(-lo.startTime.Millisecond);
                        DateTime lem = lo.endTime.AddSeconds(-lo.endTime.Second).AddMilliseconds(-lo.endTime.Millisecond);
                        DateTime ett = lo.endTime.AddMilliseconds(-lo.endTime.Millisecond);
                        DateTime stt = lo.startTime.AddMilliseconds(-lo.startTime.Millisecond);//.AddSeconds(-lo.startTime.Second).AddMinutes(1);

                        LenaOnset nlo = new LenaOnset();
                        double nloSecs = 0;
                        c++;
                        while (lsm != lem)
                        {
                             

                            nlo = new LenaOnset();
                            nloSecs = (lsm.AddMinutes(1) - stt).TotalSeconds;
                            nlo.count = lcs * nloSecs;
                            nlo.durSecs = lds * nloSecs;
                            nlo.subjectType = lo.subjectType;
                            nlo.type = lo.type;
                            if(nlo.count>0 || nlo.durSecs>0)
                            {
                                if (!loms.ContainsKey(lsm))
                                    loms.Add(lsm, new List<LenaOnset>());
                                loms[lsm].Add(lo);
                            }
                            

                            lsm = lsm.AddMinutes(1);
                            stt = lsm;//.AddMinutes(1);
                        }

                         

                        
                        nloSecs = (ett - stt).TotalSeconds;
                        if (nloSecs != 0)
                        {
                            nlo = new LenaOnset();
                            nlo.count = lcs * nloSecs;
                            nlo.durSecs = lds * nloSecs;
                            if (nlo.count > 0 || nlo.durSecs > 0)
                            {
                                if (!loms.ContainsKey(lsm))
                                    loms.Add(lsm, new List<LenaOnset>());

                                nlo.subjectType = lo.subjectType;
                                nlo.type = lo.type;
                                loms[lsm].Add(lo);
                            }
                             
                        }

                    }
                }
            }








            //Dictionary<String, int> childStamps = new Dictionary<string, int>();
            foreach (Activity act in logActivities)
            {
                String[] children = act.szChildren.Split('|');

                foreach (String child in children)
                {
                    if (child == p)
                    {
                        double tcc = 0;
                        double svc = 0;
                        double suc = 0;
                        double scc = 0;
                        double sac = 0;
                        double solnc = 0;


                        double tcd = 0;
                        double svd = 0;
                        double sud = 0;
                        double scd = 0;
                        double sad = 0;
                        double solnd = 0;
                        double cxd = 0;

                        DateTime stt = act.start;
                        while (stt < act.end)
                        {
                            try
                            {
                                List<LenaOnset> los = loms[stt];
                                foreach (LenaOnset lo in los)
                                {
                                    switch (lo.type)
                                    {
                                        case "Conversation_turnTaking":
                                            tcc += lo.count;
                                            tcd += lo.durSecs;
                                            break;
                                        case "CHN_CHF SegmentUtt":
                                            svc += lo.count;
                                            svd += lo.durSecs;
                                            break;
                                        case "FAN SegmentUtt":
                                        case "MAN SegmentUtt":
                                            sac += lo.count;
                                            sad += lo.durSecs;
                                            break;
                                        case "CHN_CHF Utt":
                                            suc += lo.count;
                                            sud += lo.durSecs;
                                            break;
                                        case
                                        "CHN_CHF Cry":
                                            scc += lo.count;
                                            scd += lo.durSecs;
                                            break;
                                        case "CXN_XF SegmentUtt":
                                            cxd += lo.durSecs;
                                            break;

                                    }

                                }

                            }
                            catch (Exception e)
                            {

                            }

                            stt=stt.AddMinutes(1);
                        }
                        sw.WriteLine(act.line + "," +
                                   child + "," +
                                   tcc + "," + tcd + "," +
                                   svc + "," + svd + "," +
                                   suc + "," + sud + "," +
                                   scc + "," + scd + "," +
                                   sac + "," + sad + "," +
                                   cxd);




                    }




                }
            }
             




        }
        public void readActivityLog(String dir)
        {
            String szDayFolder = Utilities.getDateDashStr(classDay);

            if (Directory.Exists(dir + "//" + szDayFolder + "//Activity_Log//"))
            {
                string[] szActivityLogs = Directory.GetFiles(dir + "//" + szDayFolder + "//Activity_Log//");

                foreach (string szActivityLog in szActivityLogs)
                {
                    using (StreamReader sr = new StreamReader(szActivityLog))
                    {
                        if (!sr.EndOfStream)
                        {
                            sr.ReadLine();
                        }
                        while (!sr.EndOfStream)
                        {
                            String szLine = sr.ReadLine();
                            String[] line = szLine.Split(',');
                            if (line.Length >= 8 && line[7].Trim() != "")
                            {
                                //Date (DD/MM/YY)	Children_Included	
                                //Teachers_Included	Start Time (HH:MM)	End Time (HH:MM)	
                                //Number of Children	Number of Adults	Activity	AcLog_Comments			Questions	
                                Activity act = new Activity();
                                act.line = szLine;
                                act.start = Convert.ToDateTime(line[4].Trim());

                                act.start = new DateTime(this.classDay.Year, this.classDay.Month, this.classDay.Day, act.start.Hour, act.start.Minute, 0);
                                act.end = Convert.ToDateTime(line[5].Trim());
                                act.end = new DateTime(this.classDay.Year, this.classDay.Month, this.classDay.Day, act.end.Hour, act.end.Minute, 0);
                                act.szChildren = line[2].Trim().ToUpper();
                                act.szTeachers = line[3].Trim().ToUpper();
                                //act.children = line[1].Trim().ToUpper().Split('|').ToList();
                                //act.teachers = line[2].Trim().ToUpper().Split('|').ToList();//
                                act.type = line[8].Trim().ToUpper();
                                logActivities.Add(act);

                            }
                        }
                    }
                }
            }
        }
    }
}
