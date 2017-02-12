namespace AlexaSkillProject.Repository.Migrations
{
    using Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AlexaSkillProject.Repository.AlexaSkillProjectDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AlexaSkillProject.Repository.AlexaSkillProjectDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (context.Words.Count() < 100)
            {
                context.Words.AddOrUpdate(
                    w => w.WordName,

                    new Word { WordName = "jubilation", WordOfTheDayDate = DateTime.Now },
                    new Word { WordName = "abbreviate", WordOfTheDayDate = DateTime.Now.AddDays(1) },
                    new Word { WordName = "amicable", WordOfTheDayDate = DateTime.Now.AddDays(2) },
                    new Word { WordName = "anachronistic", WordOfTheDayDate = DateTime.Now.AddDays(3) },
                    new Word { WordName = "clairvoyant", WordOfTheDayDate = DateTime.Now.AddDays(4) },
                    new Word { WordName = "condescending", WordOfTheDayDate = DateTime.Now.AddDays(5) },
                    new Word { WordName = "deleterious", WordOfTheDayDate = DateTime.Now.AddDays(6) },
                    new Word { WordName = "empathy", WordOfTheDayDate = DateTime.Now.AddDays(7) },
                    new Word { WordName = "exemplary", WordOfTheDayDate = DateTime.Now.AddDays(8) },
                    new Word { WordName = "extenuating", WordOfTheDayDate = DateTime.Now.AddDays(9) },
                    new Word { WordName = "impute", WordOfTheDayDate = DateTime.Now.AddDays(10) }

                );
            }
        }
    }
}
