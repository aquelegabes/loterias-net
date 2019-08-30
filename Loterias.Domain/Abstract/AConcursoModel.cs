using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Loterias.Common.Extensions;

namespace Loterias.Domain.Abstract
{
    public abstract class AConcursoModel : IComparer<AConcursoModel>, IComparable
    {
        [DisplayName("Id")]
        [Column("id")]
        public virtual int Id { get; set; }

        [DisplayName("Número do concurso")]
        [Column("concurso")]
        [Required]
        public int Concurso { get; set; }

        [DisplayName("Data do concurso")]
        [Column("date")]
        [Required]
        public DateTime Data { get; set; }

        [DisplayName("Acumulado ?")]
        [Column("acumulado")]
        public bool Acumulado { get; set; }

        [DisplayName("Resultado")]
        [Column("resultado")]
        [Required]
        public string Resultado { get; set; }

        [DisplayName("Valor do prêmio")]
        [Column("valor")]
        public decimal Valor { get;set; }

        [DisplayName("Quantidade de ganhadores")]
        [Column("ganhadores_concurso")]
        public int Ganhadores { get; set; }

        /// <summary>
        /// Retorna uma coleção com os resultados ordenados
        /// </summary>
        /// <value>Uma coleção com os resultados ordenados</value>
        [DisplayName("Resultado ordenado")]
        [NotMapped]
        public virtual IEnumerable<int> ResultadoOrdenado
        {
            get
            {
                var result = this.Resultado
                    .Split(s => !Char.IsDigit(s))
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
        [DisplayName("Resultado ordenado")]
        [NotMapped]
        public virtual string ResultadoOrdenadoString
        {
            get
            {
                var resultOrdered = this.ResultadoOrdenado;
                string result = string.Empty;
                foreach (var num in resultOrdered)
                {
                    result += num < 10 ? $"0{num} " : num.ToString() + " ";
                }
                return result.Trim().Replace(' ','-');
            }
        }

        #region Overrides

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Loterias.Domain.Abstract.AConcursoModel"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Loterias.Domain.Abstract.AConcursoModel"/>.</returns>
        public override string ToString()
        {
            return $"Concurso número: {this.Concurso}, realizado na data: {this.Data}";
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:Loterias.Domain.Abstract.AConcursoModel"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:Loterias.Domain.Abstract.AConcursoModel"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:Loterias.Domain.Abstract.AConcursoModel"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is AConcursoModel outro))
                return false;

            return this.Id.Equals(outro.Id)
                    || this.Concurso.Equals(outro.Concurso);
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="T:Loterias.Domain.Abstract.AConcursoModel"/> object.
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

            if (!(obj is AConcursoModel outro))
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
