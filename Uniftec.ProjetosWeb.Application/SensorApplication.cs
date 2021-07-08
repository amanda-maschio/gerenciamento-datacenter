using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniftec.ProjetosWeb.Domain.Entities;
using Uniftec.ProjetosWeb.Domain.Repository;

namespace Uniftec.ProjetosWeb.Application
{
    public class SensorApplication
    {
        private ISensorRepository SensorRepository;
        
        public SensorApplication(ISensorRepository SensorRepository)
        {
            this.SensorRepository = SensorRepository;
        }

        public Guid Inserir(Sensor Sensor)
        {
            try
            {
                Sensor.Id = Guid.NewGuid();
                SensorRepository.Inserir(Sensor);
                return Sensor.Id;
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public List<Sensor> ProcurarTodos()
        {
            try
            {
                var listaSensores = SensorRepository.SelecionarTodos();
                return listaSensores;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Guid Alterar(Sensor Sensor)
        {
            try
            {
                SensorRepository.Alterar(Sensor);
                return Sensor.Id;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool Excluir(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ApplicationException("O ID deve ser informado!");
                    //Regra de negócio a partir do repositorio
                }

                SensorRepository.Excluir(id);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Sensor Procurar(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ApplicationException("O ID deve ser informado!");
                    //Regra de negócio a partir do repositorio
                }

                var Sensores = SensorRepository.Selecionar(id);
                return Sensores;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<Sensor> ProcurarTodos(Guid id)
        {
            try
            {
                var Sensores = SensorRepository.SelecionarTodos();

                return Sensores;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
