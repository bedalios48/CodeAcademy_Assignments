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
        public async Task<MainRelative> GetMainRelative(int personId)
        {
            var person = await _personRepository.GetAsync(p => p.Id == personId);
            var mainRelative = new MainRelative(person);
            var children = await GetChildren(person.Id, "child");
            mainRelative.Relatives.AddRange(children);
            var grandChildren = children.Select(async c => await GetChildren(c.Person.Id, "grandchild"))
                .SelectMany(task => task.Result);
            mainRelative.Relatives.AddRange(grandChildren);
            var parents = await GetParents(person.Id, "parent");
            mainRelative.Relatives.AddRange(parents);
            var siblings = await GetSiblings(parents, personId);
            mainRelative.Relatives.AddRange(siblings);
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

        private async Task<IEnumerable<Relative>> GetSiblings(IEnumerable<Relative> parents, int personId)
        {
            var childrenIds = new List<int>();
            foreach (var parent in parents)
            {
                var parentChildren = await _parentChildRepository.GetAllAsync(pc => pc.ParentId == parent.Person.Id
                && pc.ChildId != personId);
                childrenIds.AddRange(parentChildren.Select(pc => pc.ChildId));
            }

            childrenIds = childrenIds.Distinct().ToList();
            var relatives = new List<Relative>();
            foreach (var childId in childrenIds)
            {
                var childParents = await _parentChildRepository.GetAllAsync(pc => pc.ChildId == childId);
                var relation = childParents.Count() == parents.Count() ? "sibling" : "half-sibling";
                var person = await _personRepository.GetAsync(p => p.Id == childId);
                var relative = new Relative(person, relation);
                relatives.Add(relative);
            }

            return relatives;
        }
    }
}
