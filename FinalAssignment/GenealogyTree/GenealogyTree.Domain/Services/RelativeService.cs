using GenealogyTree.Domain.Interfaces;
using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Models;

namespace GenealogyTree.Domain.Services
{
    public class RelativeService : IRelativeService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IRelativeServiceProvider _provider;
        public RelativeService(IPersonRepository personRepository, IRelativeServiceProvider provider)
        {
            _personRepository = personRepository;
            _provider = provider;
        }
        public async Task<MainRelative> GetMainRelativeAsync(int personId)
        {
            var person = await _personRepository.GetAsync(p => p.Id == personId);
            var mainRelative = new MainRelative(person);
            var children = await _provider.GetChildrenAsync(personId, 1);
            mainRelative.Relatives.AddRange(children);
            var grandChildren = await _provider.GetChildrenAsync(personId, 2);
            mainRelative.Relatives.AddRange(grandChildren);
            var parents = await _provider.GetParentsAsync(personId, 1);
            mainRelative.Relatives.AddRange(parents);
            var siblings = await _provider.GetSiblingsAsync(personId);
            mainRelative.Relatives.AddRange(siblings);
            var grandParents = await _provider.GetParentsAsync(personId, 2);
            mainRelative.Relatives.AddRange(grandParents);
            var spouses = await _provider.GetSpousesAsync(personId);
            mainRelative.Relatives.AddRange(spouses);
            return mainRelative;
        }
    }
}
