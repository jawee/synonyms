import { NextPage } from 'next';
import Link from 'next/link'

const FourOhFour: NextPage = () => {
  return <>
    <h1 className="text-2xl">404 - Page Not Found</h1>
    <Link href="/">
      <a>
        Go back to search
      </a>
    </Link>
  </>
}

export default FourOhFour;
