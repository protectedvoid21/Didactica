import { Button, Typography } from "@mui/material";
import { Link } from "react-router";

export const NotFound = () => {
  return (
    <div className='text-center'>
      <div className='font-bold text-[12rem] tracking-wider'>404</div>
      <Typography variant="h3" align="center" fontWeight="light" marginBlock="2rem">Nie znaleziono strony!</Typography>
      <div className='text-center'>
        Przepraszamy ale strona której szukasz nie istnieje. Sprawdź czy adres URL jest poprawny.
        <br />
        Jeśli uważasz, że to błąd, skontaktuj się z administratorem.
      </div>

      <Link to='/'>
        <Button variant="contained" sx={{margin: '2em'}}>
          Powrót na stronę główną
        </Button>
      </Link>
    </div>
  );
}