import { AllInbox, Edit, Mail, ManageAccounts, Menu, Person, PersonSearch, SettingsOutlined } from '@mui/icons-material'
import { ReactElement } from 'react'
import { Link } from 'react-router'
import { Logo } from './Logo'
import { Fab } from '@mui/material'
import { useAuth } from '../utils/AuthProvider'

export const NavBar = () => {
  const auth = useAuth();

  const NavElement = ({ href, icon, text }: { href: string, icon: ReactElement, text: string }) => (
    <Link to={href} className='flex flex-col items-center hover:transition duration-100 text-primary-800 no-underline hover:text-black group '>
      <div className='duration-200 group-hover:bg-[#E8DEF8] rounded-3xl w-2/3 p-1 mx-auto'>{icon}</div>
      <div className='no-underline'>{text}</div>
    </Link>
  )

  const elements = [
    ...(!auth.roles?.includes('WKJK') ? [
      {
        href: '/',
        icon: <PersonSearch />,
        text: 'Moje hospitacje'
      },
      {
        href: '/hospitacje-komisja',
        icon: <Person />,
        text: 'Komisja hospitacyjna'
      },
    ] : []),
    ...(auth.roles?.includes('WKJK') ? [{
      href: '/zespoly-hospitacyjne',
      icon: <ManageAccounts />,
      text: 'Zespoły hospitacyjne'
    }
    ] : []),
    {
      href: '/',
      icon: <Mail />,
      text: 'Powiadomienia'
    },
    {
      href: '/users',
      icon: <SettingsOutlined />,
      text: 'Ustawienia'
    },
    ...(auth.roles?.includes('Dean') ? [{
      href: '/zaplanowane-hospitacje',
      icon: <AllInbox />,
      text: 'Zaplanowane hospitacje'
    }] : [])
  ]

  return (
    <nav>
      <div className='mb-8'>
        <Logo />
      </div>
      <ul className='flex flex-col justify-center gap-8 my-12 text-primary-800 font-medium text-center'>
        {elements.map((element, index) => (
          <li key={index}>
            <NavElement {...element}></NavElement>
          </li>
        ))}
      </ul>
    </nav>
  )
}