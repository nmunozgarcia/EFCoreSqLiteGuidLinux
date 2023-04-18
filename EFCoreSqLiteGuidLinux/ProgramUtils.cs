using Microsoft.EntityFrameworkCore;

namespace EFCoreSqLiteGuidLinux
{
    public static class ProgramUtils
    {
        public static async Task ShowEntitiesSavedInDatabase(SampleDbContext dbContext)
        {
            List<SampleEntity> savedEntities = await dbContext.SampleEntities.ToListAsync();
            Console.WriteLine($"Amount of saved entities: {savedEntities.Count}");

            foreach (var savedEntity in savedEntities)
            {
                Console.WriteLine($"Entity with Id: {savedEntity.Id}");
            }
            Console.WriteLine();
        }
    }
}
