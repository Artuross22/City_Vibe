using City_Vibe.Services.ApiBulletinBoard;
using City_Vibe.ViewModels.ApiBulletinBoard;
using Microsoft.AspNetCore.Mvc;

namespace City_Vibe.Controllers.API
{
    public class ApiBulletinBoardController : Controller
    {
        private readonly BulletinBoardService bulletinBoardService;

        public ApiBulletinBoardController(BulletinBoardService _bulletinBoardService) => bulletinBoardService = _bulletinBoardService;


        public async Task<IActionResult> GetAllBulletinBoard()
        {
            var result = await bulletinBoardService.GetAllBulletinBoard();
            return View(result);
        }

        [HttpGet]
        public IActionResult CreateBulletinBoard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBulletinBoard(BulletinBoard messageBoardDto)
        {
            await bulletinBoardService.CreateBulletinBoard(messageBoardDto);
            return RedirectToAction("GetAllBulletinBoard");
        }

        [HttpGet]
        public async Task<IActionResult> EditBulletinBoard(string Id)
        {
            var result = await bulletinBoardService.EditBulletinBoardGet(Id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditBulletinBoard(BulletinBoard messageBoardDto)
        {
            await bulletinBoardService.EditBulletinBoardPost(messageBoardDto);
            return RedirectToAction("GetAllBulletinBoard");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBulletinBoard(string Id)
        {
          var result =  await bulletinBoardService.DeleteBulletinBoardGet(Id);
          return View(result);
        }

        [HttpPost, ActionName("DeleteBulletinBoard")]
        public async Task<IActionResult> DeleteConfirmed(string Id)
        {
            await bulletinBoardService.DeleteBulletinBoardPost(Id);
            return RedirectToAction("GetAllBulletinBoard");
        }

    }
}
