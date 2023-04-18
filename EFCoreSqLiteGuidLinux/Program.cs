// See https://aka.ms/new-console-template for more information
using EFCoreSqLiteGuidLinux;
using Microsoft.EntityFrameworkCore;

SampleDbContext dbContext = new();
await dbContext.Database.MigrateAsync();

Console.WriteLine($"Expected an empty database.");
await ProgramUtils.ShowEntitiesSavedInDatabase(dbContext);

var newEntity = new SampleEntity() { Id = Guid.NewGuid() };

dbContext.SampleEntities.Add(newEntity);
await dbContext.SaveChangesAsync();

Console.WriteLine($"Expected one entity in the database.");
await ProgramUtils.ShowEntitiesSavedInDatabase(dbContext);


SampleEntity? savedEntity1 = await dbContext.SampleEntities
    .Where(x => x.Id == newEntity.Id)
    .FirstOrDefaultAsync();
Console.WriteLine($"Has been the entity fetched from database? = {savedEntity1 is not null}");
Console.WriteLine($"In Windows the entity is fetched. In GNU/Linux the entity is not fetched");
Console.WriteLine();


SampleEntity? savedEntity2 = await dbContext.SampleEntities
    .Where(x => x.Id.ToString() == newEntity.Id.ToString())
    .FirstOrDefaultAsync();
Console.WriteLine($"Has been the entity fetched from database in the second attempt? = {savedEntity2 is not null}");
Console.WriteLine($"In GNU/Linux the entity is fetched with toString() function. In Windows the entity is not fetched (is strange in Windows do not work.)");
Console.WriteLine();

SampleEntity? savedEntity = savedEntity1 ?? savedEntity2;
if (savedEntity is not null)
{
    Console.WriteLine($"Trying to remove the entity in the database.");
    Console.WriteLine($"In Windows the entity will be removed due to Guid comparation is working. In GNU/Linux the remove call will failt because of the Linq or the query could not find the entity (the Guid comparation fail).");
    dbContext.SampleEntities.Remove(savedEntity);
    try
    {
        int rowAffected = await dbContext.SaveChangesAsync();
        Console.WriteLine($"Amount of entities deleted: {rowAffected}.");
        Console.WriteLine($"In Windows you are seeing this message. In GNU/Linux you can't because the saveChanges() function throw an exception pointing out that the entity was expected to be deleted, but that entity remains in the database.");
    }
    catch (DbUpdateConcurrencyException ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex);
        throw;
    }
}