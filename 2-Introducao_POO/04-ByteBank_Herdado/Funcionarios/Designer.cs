﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_ByteBank_Herdado.Funcionarios
{
    internal class Designer : Funcionario
    {
        public Designer(string nome, double salario, string cpf) : base(nome, 3000, cpf)
        {
            Console.WriteLine("Criando Designer");

        }

        public override void AumentarSalario()
        {
            Salario *= 1.11;
        }

        public override double GetBonificacao()
        {
            return Salario * 0.17;
        }
    }
}
