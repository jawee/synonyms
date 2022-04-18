
import { NextPage } from 'next';
import Link from 'next/link'

const FiveOhOh: NextPage = () => {
  return <>
    <h1 className="text-2xl">500 - An error occurred.</h1>
    <Link href="/">
      <a>
        Go back home
      </a>
    </Link>
  </>
}

export default FiveOhOh;
