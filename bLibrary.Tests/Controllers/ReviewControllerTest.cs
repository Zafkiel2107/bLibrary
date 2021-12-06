using bLibrary.Controllers;
using bLibrary.DBContext;
using bLibrary.Models;
using bLibrary.Models.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace bLibrary.Tests.Controllers
{
    [TestClass]
    public class ReviewControllerTest
    {
        private readonly BLibraryContext db = BLibraryContext.CreateContext();
        [TestMethod]
        [DataRow(2)]
        public async Task GetCreateEditReview(int value)
        {
            ReviewController reviewController = new ReviewController();
            ViewResult viewResult = await reviewController.GetCreateEditReview(value) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        public async Task CreateReview()
        {
            Review review = new Review
            {
                Book = db.Books.FirstOrDefault(),
                IsRecommended = true,
                Status = Status.Прочитана,
                User = db.Users.FirstOrDefault() as User,
                UserReview = "Хорошая книга"
            };
            ReviewController reviewController = new ReviewController();
            ViewResult viewResult = await reviewController.Create(review) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        public async Task EditReview()
        {
            Review review = new Review
            {
                Book = db.Books.FirstOrDefault(),
                IsRecommended = true,
                Status = Status.Прочитана,
                User = db.Users.FirstOrDefault() as User,
                UserReview = "Хорошая книга"
            };
            ReviewController reviewController = new ReviewController();
            ViewResult viewResult = await reviewController.Edit(review) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        [DataRow(2)]
        public async Task DeleteReview(int value)
        {
            ReviewController reviewController = new ReviewController();
            ViewResult viewResult = await reviewController.Delete(value) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}
