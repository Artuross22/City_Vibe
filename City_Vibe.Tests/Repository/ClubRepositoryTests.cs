using City_Vibe.Data;
using City_Vibe.Models;
using City_Vibe.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace City_Vibe.Tests.Repository
{
    public class ClubRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new ApplicationDbContext(options);

            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Club.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Club.Add(
                      new Club()
                      {
                          Title = "Hello World!",
                          Image = "https://thumbor.forbes.com/thumbor/fit-in/x/https://www.forbes.com/advisor/in/wp-content/uploads/2022/03/monkey-g412399084_1280.jpg",
                          Description = "Hello from the metaverse",

                          Category = new Category { Name = "Sport" },
                          Address = new Address()
                          {
                              Street = "Komarova",
                              City = "Cernivtsi",
                              Region = "CE",
                              ZipCode = 45
                          }
                      });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }


        [Fact]
        public async void ClubRepository_Add_ReturnsBool()
        {
            //Arrange
            var club = new Club()
            {
                Title = "Hello World!",
                Image = "https://thumbor.forbes.com/thumbor/fit-in/x/https://www.forbes.com/advisor/in/wp-content/uploads/2022/03/monkey-g412399084_1280.jpg",
                Description = "Hello from the metaverse",

                Category = new Category { Name = "Sport" },
                Address = new Address()
                {
                    Street = "Komarova",
                    City = "Cernivtsi",
                    Region = "Ce",
                    ZipCode = 45
                }

            };
            var dbContext = await GetDbContext();
            var clubRepository = new ClubRepository(dbContext);

            //Act
            var result = clubRepository.Add(club);

            //Assert
            result.Should().BeTrue();
        }


        [Fact]
        public async void ClubRepository_GetByIdAsync_ReturnsClub()
        {
            //Arrange
            var id = 1;
            var dbContext = await GetDbContext();
            var clubRepository = new ClubRepository(dbContext);

            //Act
            var result = clubRepository.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Club>>();
        }

        [Fact]
        public async void ClubRepository_GetAll_ReturnsList()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var clubRepository = new ClubRepository(dbContext);

            //Act
            var result = await clubRepository.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Club>>();
        }

        [Fact]
        public async void ClubRepository_SuccessfulDelete_ReturnsTrue()
        {
            //Arrange
            var club = new Club()
            {
                Title = "Hello World!",
                Image = "https://thumbor.forbes.com/thumbor/fit-in/x/https://www.forbes.com/advisor/in/wp-content/uploads/2022/03/monkey-g412399084_1280.jpg",
                Description = "Hello from the metaverse",

                Category = new Category { Name = "Sport" },
                Address = new Address()
                {
                    Street = "Komarova",
                    City = "Cernivtsi",
                    Region = "Ce",
                    ZipCode = 45
                }            
            };

            var dbContext = await GetDbContext();
            var clubRepository = new ClubRepository(dbContext);

            //Act
            clubRepository.Add(club);
            var result = clubRepository.Delete(club);
            var count = await clubRepository.GetCountAsync();

            //Assert
            result.Should().BeTrue();
            count.Should().Be(0);
        }

        [Fact]
        public async void ClubRepository_GetCountAsync_ReturnsInt()
        {
            //Arrange
            var club = new Club()
            {
                Title = "Hello World!",
                Image = "https://thumbor.forbes.com/thumbor/fit-in/x/https://www.forbes.com/advisor/in/wp-content/uploads/2022/03/monkey-g412399084_1280.jpg",
                Description = "Hello from the metaverse",

                Category = new Category { Name = "Sport" },
                Address = new Address()
                {
                    Street = "Komarova",
                    City = "Cernivtsi",
                    Region = "CE",
                    ZipCode = 45
                }
            };
            var dbContext = await GetDbContext();
            var clubRepository = new ClubRepository(dbContext);

            //Act
            clubRepository.Add(club);
            var result = await clubRepository.GetCountAsync();

            //Assert
            result.Should().Be(1);
        }

        [Fact]
        public async void ClubRepository_GetClubsByEventId_ReturnsList()
        {
            //Arrange
            int id = 1;
            var dbContext = await GetDbContext();
            var clubRepository = new ClubRepository(dbContext);

            //Act
            var result = await clubRepository.GetClubsByEventId(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Event>>();
        }

        [Fact]
        public async void ClubRepository_GetClubsByСity_ReturnsList()
        {
            //Arrange
            var сity = "Cernivtsi";
            var club = new Club()
            {
                Title = "Hello World!",
                Image = "https://thumbor.forbes.com/thumbor/fit-in/x/https://www.forbes.com/advisor/in/wp-content/uploads/2022/03/monkey-g412399084_1280.jpg",
                Description = "Hello from the metaverse",

                Category = new Category { Name = "Sport" },
                Address = new Address()
                {
                    Street = "Komarova",
                    City = "Cernivtsi",
                    Region = "CE",
                    ZipCode = 45
                }

            };
        
            var dbContext = await GetDbContext();
            var clubRepository = new ClubRepository(dbContext);

            //Act
            clubRepository.Add(club);
            var result = await clubRepository.GetClubByCity(сity);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Club>>();
            result.First().Title.Should().Be("Hello World!");
        }

    }
}

