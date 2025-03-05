using System;

namespace Bootcamp.LaboBackEnd.Domain;

public class Utilisateur
{
	public Guid Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime DateInscription { get; set; }
    public bool IsAdmin { get; set; }
}
