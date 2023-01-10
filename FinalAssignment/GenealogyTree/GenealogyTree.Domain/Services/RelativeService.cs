using GenealogyTree.Domain.Interfaces;
using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Models;

namespace GenealogyTree.Domain.Services
{
    public class RelativeService : IRelativeService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IParentChildRepository _parentChildRepository;
        public RelativeService(IPersonRepository personRepository, IParentChildRepository parentChildRepository)
        {
            _personRepository = personRepository;
            _parentChildRepository = parentChildRepository;
        }
        public async Task<MainRelative> GetMainRelative(int key)
        {
            var person = await _personRepository.GetAsync(p => p.UserId == key);
            var mainRelative = new MainRelative(person);
            var children = await GetChildren(person.Id, "child");
            mainRelative.Relatives.AddRange(children);
            var grandChildren = children.Select(async c => await GetChildren(c.Person.Id, "grandchild"))
                .SelectMany(task => task.Result);
            mainRelative.Relatives.AddRange(grandChildren);
            var parents = await GetParents(person.Id, "parent");
            mainRelative.Relatives.AddRange(parents);
            var grandParents = parents.Select(async c => await GetParents(c.Person.Id, "grandparent"))
                .SelectMany(task => task.Result); ;
            mainRelative.Relatives.AddRange(grandParents);
            return mainRelative;
        }

        private async Task<IEnumerable<Relative>> GetChildren(int parentId, string relation)
        {
            var parentChildren = await _parentChildRepository.GetAllAsync(pc => pc.ParentId == parentId,
                pc => pc.Child);
            return parentChildren.Select(pc => new Relative(pc.Child, relation));
        }

        private async Task<IEnumerable<Relative>> GetParents(int childId, string relation)
        {
            var parentChildren = await _parentChildRepository.GetAllAsync(pc => pc.ChildId == childId,
                pc => pc.Parent);
            return parentChildren.Select(pc => new Relative(pc.Parent, relation));
        }
    }
}
