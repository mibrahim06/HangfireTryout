namespace HangFireCronJob.HangFireJobs;

public class DbJob
{
    public void Run()
    {
        var CurrentTime = DateTime.Now;
        Console.WriteLine($"DbJob Run at {CurrentTime}");
    }
}