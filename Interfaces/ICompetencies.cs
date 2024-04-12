using AppraisalManagentSystem.Models;

namespace AppraisalManagentSystem.Interfaces
{
    public interface ICompetencies
    {

        public int SaveCompetencies(Competencies competencies);

        public List<Competencies> GetCompetencies();

        public List<string> GetCompetencyName();
    }
}
