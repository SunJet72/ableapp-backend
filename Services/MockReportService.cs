using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Services;

public class MockReportService : IReportService
{
    Dictionary<string, int> ReportDB;
    public MockReportService()
    {
        ReportDB = new Dictionary<string, int>();
    }

    public void AddReport(string adress){
        if(adress == null || adress == ""){
            return;
        }
        if(!ReportDB.Keys.Contains(adress)){
            ReportDB.Add(adress, 0);
        }
        ReportDB[adress]++;
        return;
    }

    public int GetReport(string adress){
        if(adress == null || adress == ""){
            return 0;
        }
        if(!ReportDB.Keys.Contains(adress)){
            return 0;
        }
        return ReportDB[adress];
    }

}