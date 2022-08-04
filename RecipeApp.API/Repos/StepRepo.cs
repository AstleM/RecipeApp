using RecipeApp.API.Contracts.Repos;
using RecipeApp.API.Data;

namespace RecipeApp.API.Repos
{
    public class StepRepo : BaseRepo<Step>, IStepRepo
    {
        public StepRepo(RecipeDbContext context) : base(context)
        {
        }
    }
}
