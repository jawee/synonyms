import { NextPage } from 'next';
import Link from 'next/link'

const Error: NextPage = () => {
  return <>
    <h1 className="text-2xl">An error occurred.</h1>
    <Link href="/">
      <a>
        Go back to search
      </a>
    </Link>
  </>
}

export default Error;
