using City_Vibe.Application.Interfaces;
using City_Vibe.Contracts;
using City_Vibe.Controllers;
using City_Vibe.Domain.Models;
using City_Vibe.RequestModel.Club;
using City_Vibe.ViewModels.ClubController;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace City_Vibe.Tests.Controllers
{
    public class ClubControllerTest
    {

        private ClubController clubController;
        private readonly IClubService clubService;
        private readonly IUnitOfWork unitOfWorkRepository;



        public ClubControllerTest()
        {
           // photoService = A.Fake<IPhotoService>();
            clubService = A.Fake<IClubService>();
            unitOfWorkRepository = A.Fake<IUnitOfWork>();

            //SUT
               clubController = new ClubController(clubService, unitOfWorkRepository);
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

            BaseRequestClubModel baseRequestClubModel = new BaseRequestClubModel { Id = id };
    
            var club = A.Fake<Club>();
            A.CallTo(() => unitOfWorkRepository.ClubRepository.GetByIdAsync(id)).Returns(club);

            var postInfoInClub = A.Fake<IEnumerable<PostInfoInClub>>();
            A.CallTo(() => unitOfWorkRepository.ClubRepository.GetPostInfoInClubByClubId(id)).Returns(postInfoInClub);

            //Act
            var resultGetClubsByEventId = clubController.DetailClub(baseRequestClubModel);
            var resultFindClubsByIdAsync = clubController.DetailClub(baseRequestClubModel);
            var resultGetByIdAsync = clubController.DetailClub(baseRequestClubModel);
            var resultpostInfoInClub = clubController.DetailClub(baseRequestClubModel);

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
            BaseRequestClubModel baseRequestClubModel = new BaseRequestClubModel { Id = id };

            A.CallTo(() => unitOfWorkRepository.ClubRepository.GetByIdAsync(id)).Returns(club);

            //Act
            var resultGetByIdAsyncNoTracking =  clubController.EditClub(baseRequestClubModel);
            var result = clubController.EditClub(baseRequestClubModel);

            //Assert
            resultGetByIdAsyncNoTracking.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void ClubController_EditClubPost_ReturnsSuccess()
        {
            //Arrange
            var id = 1;
            var club = A.Fake<Club>();
            var editClubViewModel = A.Fake<EditClubViewModel>();

            A.CallTo(() => unitOfWorkRepository.ClubRepository.GetByIdAsyncNoTracking(id)).Returns(club);
            //Act
            var result = clubController.EditClub(editClubViewModel);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ClubController_PostInformationDetail_ReturnsSuccess()
        {
            //Arrange
            var id = 1;
            var postInfo = A.Fake<PostInfoInClub>();

            A.CallTo(() => unitOfWorkRepository.ClubRepository.FindByIdPostInfo(id)).Returns(postInfo);
            //Act

            var result = clubController.PostInformationDetail(id);

            //Assert
            result.Should().BeOfType<Task<ActionResult>>();
        }
    }
}

