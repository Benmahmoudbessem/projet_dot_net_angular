import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Utilisateur } from '../../models/utilisateur.model';
import { UtilisateurService } from '../../services/utilisateur.service';

@Component({
  selector: 'app-liste-utilisateurs',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './liste-utilisateurs.component.html',
  
 
})
export class ListeUtilisateursComponent implements OnInit {
  utilisateurs: Utilisateur[] = [];
  utilisateurSelectionne: Utilisateur | null = null;
  nouvelUtilisateur: Utilisateur = {
    nom: '',
    prenom: '',
    email: '',
    telephone: '',
    motDePasse: '',
    adresse: '',
    estActif: true
  };
  modeEdition = false;

  constructor(private utilisateurService: UtilisateurService) { }

  ngOnInit(): void {
    this.chargerUtilisateurs();
  }

  chargerUtilisateurs(): void {
    this.utilisateurService.getUtilisateurs()
      .subscribe(
        utilisateurs => this.utilisateurs = utilisateurs,
        error => console.error('Erreur lors du chargement des utilisateurs', error)
      );
  }

  selectionnerUtilisateur(utilisateur: Utilisateur): void {
    this.utilisateurSelectionne = { ...utilisateur };
    this.modeEdition = true;
  }

  ajouterUtilisateur(): void {
    this.utilisateurService.ajouterUtilisateur(this.nouvelUtilisateur)
      .subscribe(
        utilisateur => {
          this.utilisateurs.push(utilisateur);
          this.nouvelUtilisateur = {
            nom: '',
            prenom: '',
            email: '',
            telephone: '',
            motDePasse: '',
            adresse: '',
            estActif: true
          };
        },
        error => console.error('Erreur lors de l\'ajout de l\'utilisateur', error)
      );
  }

  mettreAJourUtilisateur(): void {
    if (this.utilisateurSelectionne) {
      this.utilisateurService.modifierUtilisateur(this.utilisateurSelectionne)
        .subscribe(
          () => {
            const index = this.utilisateurs.findIndex(u => u.id === this.utilisateurSelectionne!.id);
            if (index !== -1) {
              this.utilisateurs[index] = { ...this.utilisateurSelectionne! };
            }
            this.annulerEdition();
          },
          error => console.error('Erreur lors de la mise à jour de l\'utilisateur', error)
        );
    }
  }

  supprimerUtilisateur(id: number): void {
    this.utilisateurService.supprimerUtilisateur(id)
      .subscribe(
        () => {
          this.utilisateurs = this.utilisateurs.filter(u => u.id !== id);
          if (this.utilisateurSelectionne && this.utilisateurSelectionne.id === id) {
            this.annulerEdition();
          }
        },
        error => console.error('Erreur lors de la suppression de l\'utilisateur', error)
      );
  }

  annulerEdition(): void {
    this.utilisateurSelectionne = null;
    this.modeEdition = false;
  }
}
