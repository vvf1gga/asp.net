using Kyrsova;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kyrsova.Models
{
    public class Event
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Название события обязательно.")]
        [MinLength(1, ErrorMessage = "Название события должно содержать хотя бы 1 символ.")]
        public string EventName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание события обязательно.")]
        [MinLength(1, ErrorMessage = "Описание события должно содержать хотя бы 1 символ.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Дата и время начала обязательны.")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "Дата и время начала не могут быть в прошлом.")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "Дата и время завершения обязательны.")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "Дата и время завершения должны быть после начала.")]
        public DateTime EndDateTime { get; set; }

        [Required(ErrorMessage = "Место обязательно.")]
        [MinLength(1, ErrorMessage = "Место должно содержать хотя бы 1 символ.")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Количество участников обязательно.")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество участников должно быть больше 0.")]
        public int ParticipantCount { get; set; }
    }
}
