import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Emprunt } from '../../models/emprunt.model';
import { Livre } from '../../models/livre.model';
import { Utilisateur } from '../../models/utilisateur.model';
import { EmpruntService } from '../../services/emprunt.service';
import { LivreService } from '../../services/livre.service';
import { UtilisateurService } from '../../services/utilisateur.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-liste-emprunts',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './liste-emprunts.component.html',
 
})
export class ListeEmpruntsComponent implements OnInit {
  emprunts: Emprunt[] = [];
  livres: Livre[] = [];
  utilisateurs: Utilisateur[] = [];
  nouvelEmprunt: Emprunt = {
    livreId: 0,
    utilisateurId: 0,
    dateEmprunt: new Date(),
    dateRetourPrevue: new Date(new Date().setDate(new Date().getDate() + 14)),
    estRendu: false
  };

  constructor(
    private empruntService: EmpruntService,
    private livreService: LivreService,
    private utilisateurService: UtilisateurService
  ) { }

  ngOnInit(): void {
    this.chargerEmprunts();
    this.chargerLivres();
    this.chargerUtilisateurs();
  }

  chargerEmprunts(): void {
    this.empruntService.getEmprunts()
      .subscribe(
        emprunts => this.emprunts = emprunts,
        error => console.error('Erreur lors du chargement des emprunts', error)
      );
  }

  chargerLivres(): void {
    this.livreService.getLivres()
      .subscribe(
        livres => this.livres = livres,
        error => console.error('Erreur lors du chargement des livres', error)
      );
  }

  chargerUtilisateurs(): void {
    this.utilisateurService.getUtilisateurs()
      .subscribe(
        utilisateurs => this.utilisateurs = utilisateurs,
        error => console.error('Erreur lors du chargement des utilisateurs', error)
      );
  }

  ajouterEmprunt(): void {
    this.empruntService.ajouterEmprunt(this.nouvelEmprunt)
      .subscribe(
        emprunt => {
          this.emprunts.push(emprunt);
          this.nouvelEmprunt = {
            livreId: 0,
            utilisateurId: 0,
            dateEmprunt: new Date(),
            dateRetourPrevue: new Date(new Date().setDate(new Date().getDate() + 14)),
            estRendu: false
          };
          this.chargerLivres(); // Mettre à jour les exemplaires disponibles
        },
        error => console.error('Erreur lors de l\'ajout de l\'emprunt', error)
      );
  }

  retournerEmprunt(id: number): void {
    this.empruntService.retournerEmprunt(id)
      .subscribe(
        emprunt => {
          const index = this.emprunts.findIndex(e => e.id === id);
          if (index !== -1) {
            this.emprunts[index] = emprunt;
          }
          this.chargerLivres(); // Mettre à jour les exemplaires disponibles
        },
        error => console.error('Erreur lors du retour de l\'emprunt', error)
      );
  }

  supprimerEmprunt(id: number): void {
    this.empruntService.supprimerEmprunt(id)
      .subscribe(
        () => {
          this.emprunts = this.emprunts.filter(e => e.id !== id);
        },
        error => console.error('Erreur lors de la suppression de l\'emprunt', error)
      );
  }

  getNomLivre(livreId: number): string {
    const livre = this.livres.find(l => l.id === livreId);
    return livre ? livre.titre : 'Inconnu';
  }

  getNomUtilisateur(utilisateurId: number): string {
    const utilisateur = this.utilisateurs.find(u => u.id === utilisateurId);
    return utilisateur ? `${utilisateur.prenom} ${utilisateur.nom}` : 'Inconnu';
  }

  formatDate(date: Date | string): string {
    if (!date) return '';
    const d = new Date(date);
    return d.toLocaleDateString('fr-FR');
  }
}
