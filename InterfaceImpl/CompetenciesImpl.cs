using AppraisalManagentSystem.Data;
using AppraisalManagentSystem.Interfaces;
using AppraisalManagentSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AppraisalManagentSystem.InterfaceImpl
{
    public class CompetenciesImpl : ICompetencies
    {
        private Db _context;


        public CompetenciesImpl(Db db)
        {
            _context = db;
        }

        public List<Competencies> GetCompetencies()
        {
            return _context.Competencies.ToList();
        }

        public List<string> GetCompetencyName()
        {
            return _context.Competencies.Select(m => m.CompetencyName).ToList();
        }

        public int SaveCompetencies(Competencies competencies)
        {
            _context.Add(competencies);
            _context.SaveChanges();
            return 1;
        }
    }
}
