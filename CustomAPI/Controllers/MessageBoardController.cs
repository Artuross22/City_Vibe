using CustomAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CustomAPI.Controllers
{

        [Route("api/[controller]/[action]")]
        [ApiController]
        public class MessageBoardController : ControllerBase
        {
            private readonly IMongoCollection<BulletinBoard> mongoCollection;

            public MessageBoardController(IOptions<BoardStoreDatabaseSettings> bookStoreDatabaseSettings)
            {

                var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(
                    bookStoreDatabaseSettings.Value.DatabaseName);

                mongoCollection = mongoDatabase.GetCollection<BulletinBoard>(
                bookStoreDatabaseSettings.Value.BoardCollectionName);
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<BulletinBoard>>> GetMessageBoards()


            {
                return await mongoCollection.Find(Builders<BulletinBoard>.Filter.Empty).ToListAsync();
            }

            [HttpGet("{bulletinBoardId}")]
            public async Task<ActionResult<BulletinBoard>> GetMessageBoard(string bulletinBoardId)
            {
                var filterDefenition = Builders<BulletinBoard>.Filter.Eq(x => x.BulletinBoardId, bulletinBoardId);
                return await mongoCollection.Find(filterDefenition).SingleOrDefaultAsync();
            }

            [HttpPost]
            public async Task<IActionResult> Create(BulletinBoard bulletinBoard)
            {
                await mongoCollection.InsertOneAsync(bulletinBoard);
                return Ok();
            }

            [HttpPut]
            public async Task<ActionResult> Update(BulletinBoard bulletinBoard)
            {
                var filterDefenition = Builders<BulletinBoard>.Filter.Eq(x => x.BulletinBoardId, bulletinBoard.BulletinBoardId);
                await mongoCollection.ReplaceOneAsync(filterDefenition, bulletinBoard);
                return Ok();
            }

            [HttpDelete("{bulletinBoardId}")]
            public async Task<ActionResult> Delete(string bulletinBoardId)
            {
                var filterDefenition = Builders<BulletinBoard>.Filter.Eq(x => x.BulletinBoardId, bulletinBoardId);
                await mongoCollection.DeleteOneAsync(filterDefenition);
                return Ok();
            }
        }
}
