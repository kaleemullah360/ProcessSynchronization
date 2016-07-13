class Seat
  def initialize
    @seat_type = "empty"
    @animal_type = "none"
    @lock = Mutex.new
	end

  def seat_type=(seat_type)
    @seat_type = seat_type
	end

  def animal_type=(animal_type)
    @animal_type = animal_type
	end

  def seat_type
    @seat_type
	end

  def animal_type
    @animal_type
	end

  def available?
    @lock.locked?
	end

  def occupy!(type)
    return if seats_available.empty?
    @lock.synchronize {
      puts "Seat occupied by #{type}"
      @animal_type = type
      @seat_type = "full"
      eat()
      @animal_type = "none"
      @seat_type = "empty"
    }
	end

  def eat
    sleep(30)
  end
end

class Table
  def initialize(seats_count = 1)
		@seats = []
	end
  def create_seats(seats_count = 1)
    seats_count.times do
      @seats << Seat.new
    end
  end
  def seats
    @seats
  end
  def seats_available
    @seats.collect do |seat|
      seat if seat.available?
    end.compact
  end
  def no_shark
    !@seats.collect(&:animal_type).include?("shark")
  end
  def no_fish
    !@seats.collect(&:animal_type).include?("fish")
  end
  def fish_count
    @seats.collect(&:animal_type).count("fish")
  end
end

class Animal
  attr_accessor :status
  READY_TO_GO = 0
  SIT = 1
  EAT = 2
  WAIT = 3
  READY = 4
  FINISH = 5

  def initialize
    self.status = Animal::READY_TO_GO
  end
  def shark_fish_sync_cleanup
  end

  def eat_weed(number = 1)
    puts "#{self.class.to_s.downcase} no #{number} is eating weed this time\n"
    sleep_and_wake(number)
  end

  def sleep_and_wake(number)
    self.status = Animal::EAT
    sleep(1)
    self.status = Animal::FINISH
		puts "#{self.class.to_s.downcase} no #{number} is finishing the job\n"
  end

end

class Fish < Animal
  @@fish_number = 0
  attr_accessor :fish_no
  def initialize
    super
    self.fish_no = @@fish_number += 1
  end

  def fish_before_eat(table)
    if table.seats_available and table.no_shark
      eat_weed(fish_no)
    else
      #wait
    end
      self.status = Animal::WAIT
    end

  def fish_after_eat

  end
end

class Shark < Animal
  @@shark_number = 0
  attr_accessor :shark_no

  def initialize
    super
    self.shark_no = @@shark_number +=1
  end

  def shark_before_eat(table)
    puts table
    if table.seats_available and table.no_fish
      eat_weed(shark_no)
    elsif table.fish_count == 1
      fish = table.fishes.collect{|fish| fish.status == 'FINISH'}
      puts "#{self.class.to_s.downcase} no #{self.shark_no} is eating fish #{fish.fish_no} this time\n"
      table.fishes.delete(fish)
    elsif table.fish_count > 1
      self.status = Animal::WAIT
    end
  end

  def eat_weed(number)
    to_print = ["#{self.class.to_s.downcase} no #{number} is eating weed this time\n",
      "Yayy!! #{self.class.to_s.downcase} no #{number} is eating Fish from the table\n"]
    sleep_and_wake(to_print.sample)
  end

  def shark_after_eat(table)

  end
end

class Syncproblem
  def initialize(seats, sharks, fishes, iter)
    @table = Table.new
    @table.create_seats(seats)
    @sharks = []
    sharks.times do
      @sharks << Shark.new
    end
    @fishes = []
    fishes.times do
      @fishes << Fish.new
    end
  end
  def table
    @table
  end
  def sharks
    @sharks
  end
  def fishes
    @fishes
  end
end
