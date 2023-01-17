using GenealogyTree.Domain.Interfaces;
using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Models;
using GenealogyTree.Domain.Services;
using Moq;
using System.Linq.Expressions;

namespace GenealogyTree.Tests
{
    [TestClass()]
    public class RelativeServiceTests
    {
        private Mock<IPersonRepository> _mockPersonRepo;
        private Mock<IRelativeServiceProvider> _mockProvider;
        private RelativeService sut;

        [TestInitialize]
        public void Initialize()
        {
            _mockPersonRepo = new Mock<IPersonRepository>();
            _mockProvider = new Mock<IRelativeServiceProvider>();
            sut = new RelativeService(_mockPersonRepo.Object, _mockProvider.Object);
        }

        [TestMethod]
        public async Task GetMainRelative_ReturnsPerson()
        {
            var expected = "test name";
            _mockPersonRepo.Setup(p => p
            .GetAsync(It.IsAny<Expression<Func<Person, bool>>>(), true))
                .ReturnsAsync(new Person
            {
                Name = expected
            });
            var actual = await sut.GetMainRelativeAsync(1);
            Assert.AreEqual(expected, actual.Person.Name);
        }

        [DataRow("parent", 1)]
        [DataRow("grandparent", 2)]
        //[DataRow("great-grandparent", 3)]
        //[DataRow("great-great-grandparent", 4)]
        [TestMethod]
        public async Task GetMainRelative_ReturnsParent(string expectedRelation, int generation)
        {
            var expectedName = "test name";
            _mockProvider.Setup(p => p
            .GetParentsAsync(It.IsAny<int>(), generation))
                .ReturnsAsync(
                new List<Relative>
                {
                    new Relative(
                        new Person
                        {
                            Name=expectedName
                        },
                        expectedRelation)
                });
            var actual = await sut.GetMainRelativeAsync(1);
            Assert.IsTrue(actual.Relatives
                .Any(r => r.Person.Name == expectedName
                && r.Relation == expectedRelation));
        }

        [DataRow("child", 1)]
        [DataRow("grandchild", 2)]
        [TestMethod]
        public async Task GetMainRelative_ReturnsChild(string expectedRelation, int generation)
        {
            var expectedName = "test name";
            _mockProvider.Setup(p => p
            .GetChildrenAsync(It.IsAny<int>(), generation))
                .ReturnsAsync(
                new List<Relative>
                {
                    new Relative(
                        new Person
                        {
                            Name=expectedName
                        },
                        expectedRelation)
                });
            var actual = await sut.GetMainRelativeAsync(1);
            Assert.IsTrue(actual.Relatives
                .Any(r => r.Person.Name == expectedName
                && r.Relation == expectedRelation));
        }

        public async Task GetMainRelative_ReturnsSibling()
        {
            var expectedName = "test name";
            var expectedRelation = "sibling";
            _mockProvider.Setup(p => p
            .GetSiblingsAsync(It.IsAny<int>()))
                .ReturnsAsync(
                new List<Relative>
                {
                    new Relative(new Person
                    {
                        Name = expectedName
                    }, expectedRelation)
                });
            var actual = await sut.GetMainRelativeAsync(1);
            Assert.IsTrue(actual.Relatives
                .Any(r => r.Person.Name == expectedName
                && r.Relation == expectedRelation));
        }
    }
}
