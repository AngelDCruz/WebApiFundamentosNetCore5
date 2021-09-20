namespace WebApiFundamentos.Models
{
    public class AutorLibro
    {
        public int Id { get; set; }

        public int AutorId {get; set; }

        public Autores Autor { get; set; }

        public int LibroId { get; set; }

        public Libros Libro { get; set; }

    }
}
