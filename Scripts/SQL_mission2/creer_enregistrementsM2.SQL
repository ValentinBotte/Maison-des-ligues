-- -----------------------------------------------------------------------------
--             G�n�ration d'une base de donn�es pour
--                      Oracle Version 11g XE
--                        
-- -----------------------------------------------------------------------------
--      Projet : MaisonDesLigues
--      Auteur : Beno�t ROCHE
-- 		Modification par : Dylan RAIMON
--      Date de derni�re modification : 06/10/2016 09:36:00
-- -----------------------------------------------------------------------------

-- -----------------------------------------------------------------------------
--      Partie 3: Cr�ation de l'utilisateur MDL qui sera aussi le propri�taire
-- 		des objets : tables, index, proc�dures
--
--		Ce script doit �tre ex�cut� par un l'utilisateur MDL
--		(celui qui a �t� cr�� dans le script creer_user)
-- 	Ce script doit �tre ex�cut� APRES le script creer_objets.sql
--- -----------------------------------------------------------------------------
-- -----------------------------------------------------------------------------
--


drop sequence  SEQQUALITE;
drop public  synonym SEQQUALITE;
create sequence SEQQUALITE
  increment by 1
  start with 1;
create public synonym SEQQUALITE for SEQQUALITE;

drop sequence  SEQPAIEMENT;
drop public  synonym SEQPAIEMENT;
create sequence SEQPAIEMENT
  increment by 1
  start with 1;
create public synonym SEQPAIEMENT for SEQPAIEMENT;


drop sequence  SEQATELIER;
drop public  synonym SEQATELIER;
create sequence SEQATELIER
  increment by 1
  start with 1;
create public synonym SEQATELIER for SEQATELIER;


drop sequence  SEQPARTICIPANT;
drop public synonym SEQPARTICIPANT;
create sequence SEQPARTICIPANT
  increment by 1
  start with 1;
create public synonym SEQPARTICIPANT for SEQPARTICIPANT;  
  
  
  
drop sequence SEQRESTAURATION ;
drop public synonym SEQRESTAURATION;
create sequence SEQRESTAURATION
  increment by 1
  start with 1;
create public synonym SEQRESTAURATION for SEQRESTAURATION;  

  
drop sequence SEQDATEBENEVOLAT ;
drop public synonym SEQDATEBENEVOLAT;
create sequence SEQDATEBENEVOLAT
  increment by 1
  start with 1;
create public synonym SEQDATEBENEVOLAT for SEQDATEBENEVOLAT;  

drop sequence SEQDATENUITE ;
drop public synonym SEQDATENUITE;
create sequence SEQDATENUITE
  increment by 1
  start with 1;  
create public synonym SEQDATENUITE for SEQDATENUITE;  
/*
Suppression des enregistrements de TOUTES les tables
Permet de recharger les tables sans r��x�cuter tous les scripts
*/
Delete from PARAMETRES;
Delete from CONTENUHEBERGEMENT;
Delete from INCLUREACCOMPAGNANT;
Delete from INSCRIRE;
Delete from PAIEMENT;
Delete from INTERVENANT;
Delete from LICENCIE;
Delete from ETREPRESENT;
Delete from BENEVOLE;
Delete from PROPOSER;
Delete from PARTICIPANT;
Delete from VACATION;
Delete from ATELIER;
Delete from DATENUITE;
Delete from DATEBENEVOLAT;
Delete from HOTEL;
Delete from RESTAURATION;
Delete from CATEGORIECHAMBRE;
Delete from QUALITE;
Delete from THEME;
Delete from PARTICIPER;
Delete from STATUT;



-----------------------
--   Fin du vidage des tables
---
-------------------------------
-- -----------------------------------------------------------------------------
--       TABLE : PARAMETRES
--
delete from PARAMETRES;
Insert into PARAMETRES (NOMDDL,ADRUE1,ADRUE2,CP,VILLE,TEL,FAX,MAIL,TARIFINSCRIPTION,TARIFREPASACCOMPAGNANT,DUREE,NBATELIERMAX,COMPTESMTP,PWDSMTP,EMETTEURMAIL,EMETTEURNOMLONG,PORTSMTP) values ('maison des Ligues','123 avenue des balladins','r�sidence du cirque','83000','Toulon','04 04 04 04 04 ','04 04 04 04 01 ','xxx@xxx.fr','100','35','90','6','xxxxxxxxxxx','xxxxxxxx','xxxxxx','Maison des ligues','587');

-- -----------------------------------------------------------------------------
--       TABLE : CATEGORIECHAMBRE
-- 
insert into CATEGORIECHAMBRE (ID, LIBELLECATEGORIE) values ('S', 'Single');
insert into CATEGORIECHAMBRE (ID, LIBELLECATEGORIE) values ('D', 'Double');

-- -----------------------------------------------------------------------------



-- -----------------------------------------------------------------------------
--       TABLE : DATEBENEVOLAT
-- 
insert into DATEBENEVOLAT(id,DATEBENEVOLAT) values(SEQDATEBENEVOLAT.NEXTVAL,to_timestamp('12/09/2015 09:00','DD/MM/YYYY HH24:mi') );
insert into DATEBENEVOLAT(id, DATEBENEVOLAT) values(SEQDATEBENEVOLAT.NEXTVAL,to_timestamp('13/09/2015 23:59','DD/MM/YYYY HH24:mi') );

-- -----------------------------------------------------------------------------

-- -----------------------------------------------------------------------------
--       TABLE : QUALITE
-- 
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Pr�sident de ligue');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Pr�sident de club');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Pr�sident de CD');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Vice-Pr�sident de ligue');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Vice-Pr�sident de club');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Vice-Pr�sident de CD');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'secr�taire de ligue');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'secr�taire de club');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'secr�taire de CD');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Tr�sorier de ligue');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Tr�sorier de club');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Tr�sorier de CD');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Maitre d''armes');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Eleve Maitre');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Cadre Technique R�gional');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Relais');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Coordinateur de P�le');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Athl�te de Haut Niveau');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'TBenevole de Club');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Animateur');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'FFE');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Tr�sorier de CD');
insert into qualite(id, libellequalite) values (SEQQUALITE.nextval, 'Autre');

-- -----------------------------------------------------------------------------
--       TABLE : ATELIER
--
-- le nombre de places des deux derniers ateliers a �t� mis volontairement � 0 pour simuler le fait qu'ils sont complets.
insert into ATELIER(id, libelleatelier, nbplacesmaxi) values(seqatelier.nextval, 'LE CLUB ET SON PROJET', 60);
insert into ATELIER(id, libelleatelier, nbplacesmaxi) values(seqatelier.nextval, 'LE FONCTIONNEMENT DU CLUB', 80);
insert into ATELIER(id, libelleatelier, nbplacesmaxi) values(seqatelier.nextval, 'LES OUTILS � DISPOSITION ET REMIS AUX CLUBS', 110);
insert into ATELIER(id, libelleatelier, nbplacesmaxi) values(seqatelier.nextval, 'OBSERVATOIRE DES M�TIERS DE L�ESCRIME', 60);
insert into ATELIER(id, libelleatelier, nbplacesmaxi) values(seqatelier.nextval, 'I. F. F. E', 0);
insert into ATELIER(id, libelleatelier, nbplacesmaxi) values(seqatelier.nextval, 'D�VELOPPEMENT DURABLE', 0);

-- -----------------------------------------------------------------------------
--       TABLE : VACATION
-- 

insert into vacation (IDATELIER, NUMERO,HEUREDEBUT,HEUREFIN ) values (1,1,to_timestamp('12/09/2015 11:00','DD/MM/YYYY HH24:mi'),  to_timestamp('12/09/2015 12:30','DD/MM/YYYY HH24:mi'));
insert into vacation (IDATELIER, NUMERO,HEUREDEBUT,HEUREFIN ) values (1,2,to_timestamp('12/09/2015 14:00','DD/MM/YYYY HH24:mi'),  to_timestamp('12/09/2015 15:30','DD/MM/YYYY HH24:mi'));	
insert into vacation (IDATELIER, NUMERO,HEUREDEBUT,HEUREFIN ) values (2,1,to_timestamp('12/09/2015 11:00','DD/MM/YYYY HH24:mi'),  to_timestamp('12/09/2015 12:30','DD/MM/YYYY HH24:mi'));	
insert into vacation (IDATELIER, NUMERO,HEUREDEBUT,HEUREFIN ) values (2,2,to_timestamp('12/09/2015 14:00','DD/MM/YYYY HH24:mi'),  to_timestamp('12/09/2015 15:30','DD/MM/YYYY HH24:mi'));	
insert into vacation (IDATELIER, NUMERO,HEUREDEBUT,HEUREFIN ) values (3,1,to_timestamp('12/09/2015 11:00','DD/MM/YYYY HH24:mi'),  to_timestamp('12/09/2015 12:30','DD/MM/YYYY HH24:mi'));	
insert into vacation (IDATELIER, NUMERO,HEUREDEBUT,HEUREFIN ) values (3,2,to_timestamp('12/09/2015 14:00','DD/MM/YYYY HH24:mi'),  to_timestamp('12/09/2015 15:30','DD/MM/YYYY HH24:mi'));	
insert into vacation (IDATELIER, NUMERO,HEUREDEBUT,HEUREFIN ) values (4,1,to_timestamp('12/09/2015 11:00','DD/MM/YYYY HH24:mi'),  to_timestamp('12/09/2015 12:30','DD/MM/YYYY HH24:mi'));	
insert into vacation (IDATELIER, NUMERO,HEUREDEBUT,HEUREFIN ) values (4,2,to_timestamp('12/09/2015 14:00','DD/MM/YYYY HH24:mi'),  to_timestamp('12/09/2015 15:30','DD/MM/YYYY HH24:mi'));	
	

-- -----------------------------------------------------------------------------
--       TABLE : STATUT
--
insert into STATUT(id, libellestatut) values('ANI', 'Animateur');
insert into STATUT(id, libellestatut) values('INT', 'Intervenant');


-- -----------------------------------------------------------------------------
--       TABLE : RESTAURATION
--
insert into RESTAURATION(idrestauration, daterestauration, typerepas) values(seqrestauration.nextval,to_date('12/09/2015','dd/mm/yyyy'),'midi');
insert into RESTAURATION(idrestauration, daterestauration, typerepas) values(seqrestauration.nextval,to_date('12/09/2015','dd/mm/yyyy'),'soir');
insert into RESTAURATION(idrestauration, daterestauration, typerepas) values(seqrestauration.nextval,to_date('13/09/2015','dd/mm/yyyy'),'midi');

-- -----------------------------------------------------------------------------
--       TABLE : DATENUITE
--
insert into DATENUITE(ID,DATEARRIVEENUITEE) values(SEQDATENUITE.NEXTVAL,'11/09/2015');
insert into DATENUITE(ID,DATEARRIVEENUITEE) values(SEQDATENUITE.NEXTVAL,'12/09/2015');

-- -----------------------------------------------------------------------------
--       TABLE : HOTEL
--
INSERT INTO HOTEL
   (CODEHOTEL ,NOMHOTEL, ADRESSEHOTEL1,ADRESSEHOTEL2 ,CPHOTEL,VILLEHOTEL,TELHOTEL,MAILHOTEL) 
   VALUES ('IBIS', 'H�tel ibis Lille Centre Gares','29, Av Charles St-Venant','Le Forum','59000','LILLE','(+33)3/28363040','H0901@accor.com') ;
INSERT INTO HOTEL
   (CODEHOTEL ,NOMHOTEL, ADRESSEHOTEL1,ADRESSEHOTEL2 ,CPHOTEL,VILLEHOTEL,TELHOTEL,MAILHOTEL) 
   VALUES ('NOVO', 'H�tel Novotel Lille Centre Grand Place','116 rue de l''Hopital Militaire',null,'59000','LILLE','(+33)3/28385353','H0918@accor.com') ;


  
-- -----------------------------------------------------------------------------
--       TABLE : PROPOSER
-- -----------------------------------------------------------------------------
INSERT INTO PROPOSER (CODEHOTEL,IDCATEGORIE, TARIFNUITE) VALUES('IBIS','S',61.20 );   
INSERT INTO PROPOSER (CODEHOTEL,IDCATEGORIE, TARIFNUITE) VALUES('IBIS','D',62.20 ); 
INSERT INTO PROPOSER (CODEHOTEL,IDCATEGORIE, TARIFNUITE) VALUES('NOVO','S',112.00 ); 
INSERT INTO PROPOSER (CODEHOTEL,IDCATEGORIE, TARIFNUITE) VALUES('NOVO','D',122.00 );    



commit;