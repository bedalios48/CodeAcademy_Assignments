using GenealogyTree.Domain.Interfaces;
using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Models;
using System;
using System.Linq.Expressions;

namespace GenealogyTree.Domain.Services
{
    public class RelativeServiceProvider : IRelativeServiceProvider
    {
        private readonly IUnitOfWork _repo;
        public RelativeServiceProvider(IUnitOfWork repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Relative>> GetChildrenAsync(int parentId, int generation)
        {
            if(generation == 1)
            {
                var parentChildren = await _repo.ParentChild.GetAllAsync(pc => pc.ParentId == parentId,
                pc => pc.Child);
                return parentChildren.Select(pc => new Relative(pc.Child, "child"));
            }

            if(generation == 2)
            {
                var parentChildren = await _repo.ParentChild.GetAllAsync(pc => pc.ParentId == parentId);
                var grandChildren = await GetParentChildren(parentChildren, pc => pc.Child);
                return grandChildren.Select(pc => new Relative(pc.Child, "grandchild"));
            }

            if(generation >= 3)
            {
                var parentChildren = await _repo.ParentChild.GetAllAsync(pc => pc.ParentId == parentId);
                var relation = "grandchild";
                for(int i = 2; i < generation; i++)
                {
                    parentChildren = await GetParentChildren(parentChildren);
                    relation = "great-" + relation;
                }
                var grandChildren = await GetParentChildren(parentChildren, pc => pc.Child);
                return grandChildren.Select(pc => new Relative(pc.Child, relation));
            }

            return new List<Relative>();
        }

        private async Task<List<ParentChild>> GetParentChildren(IEnumerable<ParentChild> parents, Expression<Func<ParentChild, object>>? include = null)
        {
            var allChildren = new List<ParentChild>();
            foreach(var parent in parents)
            {
                var children = await _repo.ParentChild.GetAllAsync(pc => pc.ParentId == parent.ChildId, include);
                allChildren.AddRange(children);
            }
            return allChildren;
        }

        public async Task<IEnumerable<Relative>> GetParentsAsync(int childId, int generation)
        {
            if (generation == 1)
            {
                var parentChildren = await _repo.ParentChild.GetAllAsync(pc => pc.ChildId == childId,
                pc => pc.Parent);
                return parentChildren.Select(pc => new Relative(pc.Parent, "parent"));
            }

            if (generation == 2)
            {
                var parentChildren = await _repo.ParentChild.GetAllAsync(pc => pc.ChildId == childId);
                var grandParents = await GetChildrenParents(parentChildren, pc => pc.Parent);
                return grandParents.Select(pc => new Relative(pc.Parent, "grandparent"));
            }

            if (generation >= 3)
            {
                var parentChildren = await _repo.ParentChild.GetAllAsync(pc => pc.ChildId == childId);
                var relation = "grandparent";
                for (int i = 2; i < generation; i++)
                {
                    parentChildren = await GetChildrenParents(parentChildren);
                    relation = "great-" + relation;
                }
                var grandParents = await GetChildrenParents(parentChildren, pc => pc.Parent);
                return grandParents.Select(pc => new Relative(pc.Parent, relation));
            }

            return new List<Relative>();
        }

        private async Task<List<ParentChild>> GetChildrenParents(IEnumerable<ParentChild> children, Expression<Func<ParentChild, object>>? include = null)
        {
            var allParents = new List<ParentChild>();
            foreach (var child in children)
            {
                var parents = await _repo.ParentChild.GetAllAsync(pc => pc.ChildId == child.ParentId, include);
                allParents.AddRange(parents);
            }
            return allParents;
        }

        public async Task<IEnumerable<Relative>> GetSiblingsAsync(int personId)
        {
            var childrenIds = new List<int>();
            var parents = await GetParentsAsync(personId, 1);
            foreach (var parent in parents)
            {
                var parentChildren = await _repo.ParentChild.GetAllAsync(pc => pc.ParentId == parent.Person.Id
                && pc.ChildId != personId);
                childrenIds.AddRange(parentChildren.Select(pc => pc.ChildId));
            }

            childrenIds = childrenIds.Distinct().ToList();
            var relatives = new List<Relative>();
            foreach (var childId in childrenIds)
            {
                var childParents = await _repo.ParentChild.GetAllAsync(pc => pc.ChildId == childId);
                var relation = childParents.Count() == parents.Count() ? "sibling" : "half-sibling";
                var person = await _repo.Person.GetAsync(p => p.Id == childId);
                var relative = new Relative(person, relation);
                relatives.Add(relative);
            }

            return relatives;
        }

        public async Task<IEnumerable<Relative>> GetSpousesAsync(int personId)
        {
            var spouses = await _repo.Marriage.GetAllAsync(m => m.PersonId == personId, m => m.SpousePerson);
            var peopleSpouses = await _repo.Marriage.GetAllAsync(m => m.SpouseId == personId, m => m.Person);
            var relatives = new List<Relative>();
            foreach(var spouse in spouses)
            {
                if(spouse.AreDivorced)
                {
                    relatives.Add(new Relative(spouse.SpousePerson, "spouse-divorced"));
                    continue;
                }
                relatives.Add(new Relative(spouse.SpousePerson, "spouse"));
            }
            foreach (var spouse in peopleSpouses)
            {
                if (spouse.AreDivorced)
                {
                    relatives.Add(new Relative(spouse.Person, "spouse-divorced"));
                    continue;
                }
                relatives.Add(new Relative(spouse.Person, "spouse"));
            }
            return relatives;
        }
    }
}
