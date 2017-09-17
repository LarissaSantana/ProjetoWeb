using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{

    public interface IDAO<T>
    {
        List<T> ListarTodos();
        T Obter(int id);
        int Inserir(T obj);
        int Atualizar(T obj);
        int Deletar(int id);

    }
}
