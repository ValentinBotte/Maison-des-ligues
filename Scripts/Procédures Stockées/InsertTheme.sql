CREATE OR REPLACE PROCEDURE INSERTTHEME (
  numero in theme.numero%TYPE,
  libelletheme in theme.libelletheme%TYPE
)AS
BEGIN
  insert into THEME values (seqatelier.nextval, numero, libelletheme);
  EXCEPTION
    WHEN others then
      raise_application_error(-20001, 'Insertion impossible');
END INSERTTHEME;