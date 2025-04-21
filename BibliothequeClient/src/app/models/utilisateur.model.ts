export interface Utilisateur {
    id?: number;
    nom: string;
    prenom: string;
    email: string;
    telephone?: string;
    motDePasse: string;
    adresse?: string;
    dateInscription?: Date;
    estActif: boolean;
  
}
