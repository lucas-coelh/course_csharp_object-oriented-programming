﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Modelos
{
    /// <summary>
    /// Define uma Conta Corrente do banco ByteBank
    /// </summary>

    public class ContaCorrente : IComparable
    {
        private static int TaxaOperacao;

        public static int TotalDeContasCriadas { get; private set; }

        public Cliente Titular { get; set; }

        public int ContadorSaquesNaoPermitidos { get; private set; }
        public int ContadorTransferenciasNaoPermitidas { get; private set; }

        public int Numero { get; }
        public int NumeroAgencia { get; }

        private double _saldo = 100;
        public double Saldo
        {
            get
            {
                return _saldo;
            }
            set
            {
                if (value < 0)
                {
                    return;
                }

                _saldo = value;
            }
        }

        /// <summary>
        /// Cria uma instancia de ContaCorrente com os argumentos utilizados.
        /// </summary>
        /// <param name="agencia"> Representa o valor da propriedade <see cref="NumeroAgencia"/> e deve possuir um valor maior que zero.</param>
        /// <param name="numero"> Representa o valor da propriedade <see cref="Numero"/> e deve possuir um valor maior que zero.</param>
        /// <exception cref="ArgumentException"></exception>
        public ContaCorrente(int agencia, int numero)
        {
            if (numero <= 0)
            {
                throw new ArgumentException("O argumento agencia deve ser maior que 0.", nameof(agencia));
            }

            if (numero <= 0)
            {
                throw new ArgumentException("O argumento numero deve ser maior que 0.", nameof(numero));
            }

            NumeroAgencia = agencia;
            Numero = numero;

            TotalDeContasCriadas++;
            TaxaOperacao = 30 / TotalDeContasCriadas;
        }

        /// <summary>
        /// Realiza o saque e atualiza o valor da propiedade <see cref="Saldo"/>
        /// </summary>
        /// <exception cref="ArgumentException"> Exceçao lançada quando um valor negativo é utilizado no argumento <paramref name="valor"/></exception>
        /// <exception cref="SaldoInsuficienteException"> Exceçao lançada quando o valor de <paramref name="valor"/> é maior que a propiedade <paramref name="Saldo"/>o </exception>
        /// <param name="valor"> Representa o valor do saque, deve ser maior que 0 e menor que o <paramref name="Saldo"/></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="SaldoInsuficienteException"></exception>
        public void Sacar(double valor)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Valor inválido para o saque.", nameof(valor));
            }

            if (_saldo < valor)
            {
                ContadorSaquesNaoPermitidos++;
                throw new SaldoInsuficienteException(Saldo, valor);
            }

            _saldo -= valor;
        }

        public void Depositar(double valor)
        {
            _saldo += valor;
        }

        public void Transferir(double valor, ContaCorrente contaDestino)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Valor inválido para a transferência.", nameof(valor));
            }

            try
            {
                Sacar(valor);
            }
            catch (SaldoInsuficienteException ex)
            {
                ContadorTransferenciasNaoPermitidas++;
                throw new OperacaoFinanceiraException("Operação não realizada.", ex);
            }

            contaDestino.Depositar(valor);
        }

        public override string ToString()
        {
            return $"Conta: {Numero}, Agência: {NumeroAgencia}, Saldo: {Saldo}";
            
        }

        public int CompareTo(object? obj)
        {
            //Retornar negativo quando a instancia atual for menor do que a passada no argumento
            //Retornar zero quando a instancia atual for igual ao argumento
            //Retornar positivo quando a instancia atual for maior do que a passada no argumento

            var outraConta = obj as ContaCorrente;

            if (outraConta == null)
            {
                return -1;
            }

            if (Numero < outraConta.Numero)
            {
                return -1;
            }
            else if (Numero == outraConta.Numero)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }
    }

}
