namespace TestioProject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public static class SeedData
    {
        //public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        //{
            //var roleMng = serverProvader.GetRequiredService<RoleManager> < IdentityRole >>();
            //roleMng.CreateAsync(new IdentityRole { Name = "Student", NormalizedName = "STUDENT" });
            //roleMng.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
            //context.Roles.Add(new IdentityRole { Name = "Learner", NormalizedName = "LEARNER" });
            //context.Roles.Add(new IdentityRole { Name = "Teacher", NormalizedName = "TEACHER" });
            //context.Roles.Add(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
            //context.Roles.Add(new IdentityRole { Name = "TemporaryLimitedUser", NormalizedName = "TEMPORARYLIMITEDUSER" });
            //context.SaveChanges();

            //var admin = await userManager.CreateAsync(new ApplicationUser () { Email = "admin@gmail.com", FirstName = "admin", LastName = "admin", UserName = "admin@gmail.com" }, "Admin123!");
            //if(admin.Succeeded)
            //{
            //    var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");
            //    await userManager.AddToRoleAsync(adminUser, "Admin");
            //}

            //string userId = dataManager.Users.GetFirstFromDb().Id;
            //dataManager.Tests.SaveTest(new Test() { Name = "Test1", Description = "Some desc1", UserId = userId, CodeLock = "12345" });

            //dataManager.Questions.SaveQuestion(new Question() { Name = "What color is sky ?", TestId = 6 });
            //dataManager.Questions.SaveQuestion(new Question() { Name = "In what year was Lviv founded ?", TestId = 6 });

            //List<Question> questions = dataManager.Questions.GetAllQuestions().ToList();

            //dataManager.Answers.SaveAnswer(new Answer() { Text = "Blue", isTruth = true, QuestionId = questions[0].Id });
            //dataManager.Answers.SaveAnswer(new Answer() { Text = "Dark", isTruth = false, QuestionId = questions[0].Id });

            //dataManager.Answers.SaveAnswer(new Answer() { Text = "1256", isTruth = true, QuestionId = questions[1].Id });
            //dataManager.Answers.SaveAnswer(new Answer() { Text = "1154", isTruth = false, QuestionId = questions[1].Id });


            //Test test = context.Tests.Include(x => x.Questions).First();
            //Question question = context.Questions.Include(x => x.Answers).Where(x => x.TestId == test.Id).FirstOrDefault();
            //Test test = dataManager.Tests.GetTestById(6);
            //List<Question> questions = dataManager.Questions.GetQuestionsByTestId(test.Id).ToList();
            //var x = "";
        //}
    }
}
