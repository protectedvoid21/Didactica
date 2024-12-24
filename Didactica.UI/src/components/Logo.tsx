import { Link } from 'react-router';

export const Logo = () => {
  return (
    <Link to={'/'}>
      <div className='uppercase font-bold text-3xl'>Didactica</div>
    </Link>
  );
}