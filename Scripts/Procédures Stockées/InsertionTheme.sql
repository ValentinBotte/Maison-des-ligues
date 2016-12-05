create or replace procedure insertionTheme
(
  idatelier in theme.idatelier%TYPE,
  numero in theme.numero%TYPE,
  libelletheme in theme.libelletheme%TYPE
) as
begin
  insert into THEME values (idatelier, numero, libelletheme);
  EXCEPTION
    WHEN others then
      raise_application_error(-20001, 'Insertion impossible');
end insertionTheme;