using Kyrsova;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kyrsova.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва обов'язкова для заповнення!")]
        [MaxLength(31, ErrorMessage = "Навзва не повинна перебільшувати 30 символів.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание события обязательно.")]
        [MaxLength(101, ErrorMessage = "Опис не повинен перебільшувати 100 символів.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Дата и время начала обязательны.")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "Дата и время начала не могут быть в прошлом.")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "Дата и время завершения обязательны.")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "Дата и время завершения должны быть после начала.")]
        public DateTime EndDateTime { get; set; }
        public City City { get; set; }

        [Required(ErrorMessage = "Количество участников обязательно.")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество участников должно быть больше 1.")]
        public int ParticipantCount { get; set; }
    }

    public enum City
    {
        Харків,
        Київ,
        Одеса,
        Львів,
        Дніпро
    }
}
