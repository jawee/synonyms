import Link from "next/link";

const Header = () => {
    return (
        <header className="container py-4">
            <div className="max-w-3xl container flex flex-wrap justify-between items-center mx-auto">
                <h1 className="text-4xl"><Link href="/">synonyms</Link></h1>
                <nav>
                    <ul className="flex flex-col mt-4 md:flex-row md:space-x-8 md:mt-0 md:text-sm md:font-medium">
                        <li><Link href="/"><a className="hover:text-[#84A98C]">Search</a></Link></li>
                        <li><Link href="/create-synonym"><a className="hover:text-[#84A9BC]">Create Synonym</a></Link></li>
                    </ul>
                </nav>
            </div>
        </header>
    )
}

export default Header;
