import { Edit, Mail, Menu, SettingsOutlined } from '@mui/icons-material'
import { ReactElement } from 'react'
import { Link } from 'react-router'
import { Logo } from './Logo'
import { Fab } from '@mui/material'

export const NavBar = () => {
  const NavElement = ({ href, icon, text }: { href: string, icon: ReactElement, text: string }) => (
    <Link to={href} className='flex flex-col items-center text-primary-800 no-underline'>
      {icon}
      <div className='no-underline'>{text}</div>
    </Link>
  )

  const elements = [
    {
      href: '/',
      icon: <Mail />,
      text: 'Powiadomienia'
    },
    {
      href: '/users',
      icon: <SettingsOutlined />,
      text: 'Ustawienia'
    }
  ]

  return (
    <nav>
      <div className='mb-8'>
        <Logo />
      </div>
      <ul className='flex flex-col justify-center gap-8 my-12 text-primary-800 font-medium text-center'>
        <li>
          <Menu />
        </li>
        <li>
          <Fab color='primary' sx={{ borderRadius: '20%', boxShadow: 'none', }}>
            <Edit />
          </Fab>
        </li>
        {elements.map((element, index) => (
          <li key={index}>
            <NavElement {...element}></NavElement>
          </li>
        ))}
      </ul>
    </nav>
  )
}