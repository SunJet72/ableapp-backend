using TodoApi.Model;
namespace TodoApi.Services;

public class StepService{

    public const float stepSize = 0.16f;
    public StepService(){

    }

    public void CountSteps(Way way){
        foreach(TodoApi.Model.Path path in way.Paths){
            if(!path.IsSteps){
                continue;
            }
            path.StepsCount = (int)Math.Ceiling(Math.Abs(path.Points.First().Height-path.Points.Last().Height)/stepSize);
        }
    }
}