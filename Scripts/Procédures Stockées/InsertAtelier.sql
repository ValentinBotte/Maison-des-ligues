create or replace procedure insertionAtelier
(
  libelleatelier in ATELIER.LIBELLEATELIER%TYPE,
  nbplacesmaxi in ATELIER.NBPLACESMAXI%TYPE
) as
begin
  insert into ATELIER values (seqatelier.nextval, libelleatelier, nbplacesmaxi);
  EXCEPTION
    WHEN others then
      raise_application_error(-20001, 'Insertion impossible');
end insertionAtelier;