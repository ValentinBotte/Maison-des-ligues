create or replace PROCEDURE insertionVacation
(
  idAtelier in vacation.idatelier%TYPE,
  numero in vacation.numero%TYPE,
  heuredebut varchar,
  heurefin varchar
) as
begin
  insert into vacation values (idAtelier, numero, TO_DATE(heuredebut, 'DD/MM/YYYY HH24:MI:SS'), TO_DATE(heurefin, 'DD/MM/YYYY HH24:MI:SS'));
  commit;
  EXCEPTION
    WHEN others then
      raise_application_error(-20001, 'Insertion impossible');
end insertionVacation;