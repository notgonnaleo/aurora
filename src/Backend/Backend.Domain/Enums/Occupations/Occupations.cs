using Backend.Domain.Entities.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Enums.Occupations
{
    public class Occupations
    {
        public static List<AgentOccupation> OccupationList = new List<AgentOccupation>()
        {
            new AgentOccupation(992110, "Balanceador"),
            new AgentOccupation(992115, "Borracheiro"),
            new AgentOccupation(992120, "Lavador de peças"),
            new AgentOccupation(992205, "Encarregado geral de operações de conservação de vias permanentes (exceto trilhos)"),
            new AgentOccupation(992210, "Encarregado de equipe de conservação de vias permanentes (exceto trilhos)"),
            new AgentOccupation(992215, "Operador de ceifadeira na conservação de vias permanentes"),
            new AgentOccupation(992220, "Pedreiro de conservação de vias permanentes (exceto trilhos)"),
            new AgentOccupation(992225, "Auxiliar geral de conservação de vias permanentes (exceto trilhos)")
        };
    }
}
