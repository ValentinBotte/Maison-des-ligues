CREATE OR REPLACE PROCEDURE MODIFICATIONVACATION
(
  pidAtelier in vacation.idatelier%TYPE,
  pnumero in vacation.numero%TYPE,
  pheuredebut varchar,
  pheurefin varchar
) as
BEGIN
  if pidatelier is not null and pnumero is not null then
    if pheurefin is not null and pheurefin is not null then     
      update vacation
      set vacation.heuredebut = TO_DATE(pheuredebut, 'DD/MM/YYYY HH24:MI:SS'),
      vacation.heurefin = TO_DATE(pheurefin, 'DD/MM/YYYY HH24:MI:SS')
      where vacation.idatelier = pidatelier
      and vacation.numero = pnumero;
      commit;
    elsif pheuredebut is not null then
      update vacation
        set vacation.heuredebut = TO_DATE(pheuredebut, 'DD/MM/YYYY HH24:MI:SS')
        where vacation.idatelier = pidatelier
        and vacation.numero = pnumero;
        commit;
    elsif pheurefin is not null then     
      update vacation
      set vacation.heurefin = TO_DATE(pheurefin, 'DD/MM/YYYY HH24:MI:SS')
      where vacation.idatelier = pidatelier
      and vacation.numero = pnumero;
      commit;
    end if;
  end if;
  
  EXCEPTION
    WHEN others then
      raise_application_error(-20001, 'Modification impossible');
  END MODIFICATIONVACATION