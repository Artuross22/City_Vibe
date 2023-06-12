using City_Vibe.Controllers;
using City_Vibe.Data;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using City_Vibe.Repository;
using City_Vibe.Services;
using City_Vibe.ViewModels.ClubController;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace City_Vibe.Tests.Controllers
{
    public class ClubControllerTest
    {

        private ClubController clubController;

        private readonly IClubRepository clubRepository;
        private readonly IPhotoService photoService;
        private readonly ICategoryRepository categoryRepository;
        private readonly IHttpContextAccessor сontextAccessor;
        private readonly ISaveClubRepository saveClubRepository;
        private readonly IlikeClubRepository likeClubRepository;
        private readonly IClubCommentRepository clubCommentRepository;
        private readonly IUnitOfWork unitOfWorkRepository;



        public ClubControllerTest()
        {
            photoService = A.Fake<IPhotoService>();
            сontextAccessor = A.Fake<IHttpContextAccessor>();
            unitOfWorkRepository = A.Fake<IUnitOfWork>();

            //SUT
            clubController = new ClubController( photoService, сontextAccessor, unitOfWorkRepository);
        }

        [Fact]
        public void ClubController_Index_ReturnsSuccess()
        {
            //Arrange - What do i need to bring in?
            var clubs = A.Fake<IEnumerable<Club>>();
            A.CallTo(() => unitOfWorkRepository.ClubRepository.GetAll()).Returns(clubs);

            //Act
            var result = clubController.Index();
            //Assert - Object check actions
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ClubController_DetailClub_ReturnsSuccess()
        {
            //Arrange
            var id = 1;

            var club = A.Fake<Club>();
            A.CallTo(() => clubRepository.GetByIdAsync(id)).Returns(club);

            var events = A.Fake<IEnumerable<Event>>();
            A.CallTo(() => clubRepository.GetClubsByEventId(id)).Returns(events);

            var saveClub = A.Fake<IEnumerable<SaveClub>>();
            A.CallTo(() => saveClubRepository.FindClubsByIdAsync(id)).Returns(saveClub);


            var postInfoInClub = A.Fake<IEnumerable<PostInfoInClub>>();
            A.CallTo(() => clubRepository.GetPostInfoInClubByClubId(id)).Returns(postInfoInClub);

            //Act
            var resultGetClubsByEventId = clubController.DetailClub(id);
            var resultFindClubsByIdAsync = clubController.DetailClub(id);
            var resultGetByIdAsync = clubController.DetailClub(id);
            var resultpostInfoInClub = clubController.DetailClub(id);

            //Assert
            resultGetByIdAsync.Should().BeOfType<Task<IActionResult>>();
            resultGetClubsByEventId.Should().BeOfType<Task<IActionResult>>();
            resultFindClubsByIdAsync.Should().BeOfType<Task<IActionResult>>();
            resultpostInfoInClub.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ClubController_EditClubGet_ReturnsSuccess()
        {
            //Arrange
            var id = 1;
            var club = A.Fake<Club>();

            A.CallTo(() => clubRepository.GetByIdAsync(id)).Returns(club);

            //Act
            var resultGetByIdAsyncNoTracking = clubController.EditClub(id);

            //Assert
            resultGetByIdAsyncNoTracking.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ClubController_EditClubPost_ReturnsSuccess()
        {
            //Arrange
            var id = 1;
            var club = A.Fake<Club>();
            var editClubViewModel = A.Fake<EditClubViewModel>();

            A.CallTo(() => clubRepository.GetByIdAsyncNoTracking(id)).Returns(club);
            //Act
            var result = clubController.EditClub(id, editClubViewModel);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ClubController_PostInformationDetail_ReturnsSuccess()
        {
            //Arrange
            var id = 1;
            var postInfo = A.Fake<PostInfoInClub>();

            A.CallTo(() => clubRepository.FindByIdPostInfo(id)).Returns(postInfo);
            //Act

            var result = clubController.PostInformationDetail(id);

            //Assert
            result.Should().BeOfType<Task<ActionResult>>();
        }
    }
}

