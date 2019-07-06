using System;
using System.Collections.Generic;
using System.Linq;

using Loterias.Contract.Inteface;

namespace Loterias.Contract.Abstract
{
    public class AConcursoModel : IModel, IComparer<AConcursoModel>, IComparable
    {
        public long Id { get; set; }
        public int Concurso { get; set; }
        public DateTime Data { get; set; }
        public bool Acumulado { get; set; }
        public string Resultado { get; set; }
        public int Ganhadores { get; set; }

        /// <summary>
        /// Retorna uma coleção com os resultados ordenados
        /// </summary>
        /// <value>Uma coleção com os resultados ordenados</value>
        public ICollection<int> ResultadoOrdenado
        {
            get
            {
                var result = this.Resultado
                                .Split('-')
                                .Select(s => int.Parse(s))
                                .ToList();
                result.Sort();
                return result;
            }
        }

        /// <summary>
        /// Retorna uma string com os resultados ordenados
        /// </summary>
        /// <value>Uma string com os resultados ordenados</value>
        public string ResultadoOrdenadoString
        {
            get
            {
                var resultOrdered = this.ResultadoOrdenado;
                string result = string.Empty;
                foreach (var num in resultOrdered)
                {
                    result += num < 10 ? $"0{num}" : num.ToString();
                }
                return result;
            }
        }

        #region Overrides


        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Loterias.Contract.Abstract.AConcursoModel"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Loterias.Contract.Abstract.AConcursoModel"/>.</returns>
        public override string ToString()
        {
            return $"Concurso número: {this.Concurso}, realizado na data: {this.Data}";
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:Loterias.Contract.Abstract.AConcursoModel"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:Loterias.Contract.Abstract.AConcursoModel"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:Loterias.Contract.Abstract.AConcursoModel"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            AConcursoModel outro = obj as AConcursoModel;
            if (outro == null)
                return false;

            return this.Id.Equals(outro.Id)
                    || this.Concurso.Equals(outro.Concurso);
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="T:Loterias.Contract.Abstract.AConcursoModel"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Concurso);
        }
        #endregion

        #region Implementação de Interfaces

        /// <summary>
        /// Compare the specified x and y.
        /// </summary>
        /// <returns>The compare.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public int Compare(AConcursoModel x, AConcursoModel y)
        {
            if (x.Concurso == 0 || y.Concurso == 0)
                return 0;
            return x.Concurso.CompareTo(y.Concurso);
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <returns>The to.</returns>
        /// <param name="obj">Object.</param>
        /// <exception cref="ArgumentException"></exception>
        public int CompareTo(object obj)
        {
            //retorna 0 => objetos são iguais
            //retornar > 0 => objeto atual vem depois
            //retornar < 0 => objeto atual vem antes

            if (obj == null)
                return 1;

            AConcursoModel outro = obj as AConcursoModel;
            if (outro == null)
                throw new ArgumentException("Objeto não é um Concurso");

            int resultado = this.Id.CompareTo(outro.Id);
            if (resultado == 0)
            {
                resultado = this.Concurso.CompareTo(outro.Concurso);
            }
            return resultado;
        }
        #endregion Implementação de Interfaces
    }
}
