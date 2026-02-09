using AppEmpleo.Models;

namespace AppEmpleo.Interfaces.Utilities
{
    public interface IResumeFactory
    {
        Resume Create(Candidate candidate, string originalFileName, string storedFileName);
    }
}
