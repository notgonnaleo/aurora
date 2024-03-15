using Backend.Domain.Entities.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Enums.Agents
{
    public static class CNAES
    {
        public static List<CNAE> List = new List<CNAE>()
            {
                new CNAE()
                {
                    CnaeId = "3250706",
                    CnaeDescription = "Serviços de prótese dentária",
                    Annex = 5,
                    FactorR = true,
                    Aliquot = 15.5
                },
                new CNAE()
                {
                    CnaeId = "3250706",
                    CnaeDescription = "Serviços de prótese dentária",
                    Annex = 3,
                    FactorR = true,
                    Aliquot = 6
                },
                new CNAE()
                {
                    CnaeId = "3250709",
                    CnaeDescription = "Serviço de laboratório óptico",
                    Annex = 5,
                    FactorR = true,
                    Aliquot = 15.5
                },
                new CNAE()
                {
                    CnaeId = "4512901",
                    CnaeDescription = "Representantes comerciais e agentes do comércio de veículos automotores",
                    Annex = 5,
                    FactorR = true,
                    Aliquot = 15.5
                },
            };
    }
}
